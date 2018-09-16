namespace CodePathFinder.CodeAnalysis.PathFinding
{
    using ConcurrentCollections;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Parallel DFS based explore algorithm for finding code paths
    /// </summary>
    public class DepthFirstCodePathFinder : ICodePathFinder
    {
        /// <summary>
        /// The assembly analyzer to use
        /// </summary>
        private readonly IAssemblyGraphAnalyzer analyzer;

        /// <summary>
        /// The number of threads to run
        /// </summary>
        private readonly int numThreads;

        /// <summary>
        /// The the partially formed paths
        /// </summary>
        private List<CodePath> partialPaths;

        /// <summary>
        /// Marks if a method has been visited
        /// </summary>
        private ConcurrentHashSet<Method> visited;

        /// <summary>
        /// If a method ultimately has one or more paths to the solution method,
        /// they are cached here to speed up search and maintain correctness in a
        /// multi-threaded environment
        /// </summary>
        private Dictionary<Method, List<CodePath>> pathSolutionMap;

        /// <summary>
        /// Like <see cref="pathSolutionMap" /> but only a set (only the keys)
        /// </summary>
        private ConcurrentHashSet<Method> solutionMethods;

        /// <summary>
        /// Specifies which methods have had all their children explored and can 
        /// be considered "completed"
        /// </summary>
        private ConcurrentHashSet<Method> completedMethods;

        /// <summary>
        /// Maps the completion progress (i.e. how many of its children have been properly explored)
        /// of a method
        /// </summary>
        private ConcurrentDictionary<Method, MethodCompletionRing> nodeCompletionMap;

        /// <summary>
        /// If a method is marked as visited, but not "complete", it is added to the backlog
        /// to be processed later
        /// </summary>
        private ConcurrentBag<CodePath> backlog;

        /// <summary>
        /// Initializes a new instance of the <see cref="DepthFirstCodePathFinder" /> class
        /// </summary>
        /// <param name="analyzer">The assembly analyzer to use</param>
        /// <param name="numThreads">the number of search threads</param>
        public DepthFirstCodePathFinder(IAssemblyGraphAnalyzer analyzer, int numThreads = 1)
        {
            this.analyzer = analyzer;
            this.numThreads = numThreads;
        }

        /// <summary>
        /// Find all paths between two methods
        /// </summary>
        /// <param name="start">method to start from</param>
        /// <param name="end">goal method</param>
        /// <returns>all paths</returns>
        public IList<CodePath> FindPathsBetweenMethods(Method start, Method end, int maxPathLength = -1)
        {
            return EnumeratePathsBetweenMethods(start, end).ToList();
        }

        public IEnumerable<CodePath> EnumeratePathsBetweenMethods(Method start, Method end, int maxPathLength = -1)
        {
            ConstructPartialPaths(start, end).Wait();

            var pathStack = new Stack<CodePath>(this.partialPaths);
            while (pathStack.Count > 0)
            {
                var path = pathStack.Pop();
                if (path.LastMethod == end)
                {
                    yield return path;
                }
                else
                {
                    var subPaths = pathSolutionMap[path.LastMethod];
                    foreach (var subPath in subPaths)
                    {
                        if (maxPathLength > -1 &&
                            (path.Length + subPath.Length) > maxPathLength)
                        {
                            continue;
                        }

                        pathStack.Push(path.ConcatNew(subPath));
                    }
                }
            }
        }

        private async Task ConstructPartialPaths(Method start, Method end)
        {
            this.partialPaths = new List<CodePath>();
            this.visited = new ConcurrentHashSet<Method>();
            this.pathSolutionMap = new Dictionary<Method, List<CodePath>>();
            this.solutionMethods = new ConcurrentHashSet<Method>();
            this.nodeCompletionMap = new ConcurrentDictionary<Method, MethodCompletionRing>();
            this.completedMethods = new ConcurrentHashSet<Method>();
            this.backlog = new ConcurrentBag<CodePath>();

            var proctor = new StackProctor(this.numThreads);
            var stack = proctor.GetStack(0);
            stack.Push(CodePath.FromSingleMethod(start));

            Func<int, Action> curryDatShit = i => () => { RunSearchThread(end, proctor, i); };
            var tasks = new Task[this.numThreads];
            for (var i = 0; i < this.numThreads; i++)
            {
                tasks[i] = Task.Run(curryDatShit(i));
                tasks[i].ContinueWith(HandleThreadError, TaskContinuationOptions.OnlyOnFaulted);
            }

            await Task.WhenAll(tasks);
        }

        private void HandleThreadError(Task failedTask)
        {
            if (failedTask.IsFaulted && failedTask.Exception != null)
            {
                Console.WriteLine("Exception: {0}", failedTask.Exception.ToString());
            }
        }

        private void RunSearchThread(Method end, StackProctor proctor, int threadId)
        {
            var stack = proctor.GetStack(threadId);
            proctor.SetStackWaiting(threadId);

            var shouldTerminate = false;
            while (!shouldTerminate)
            {
                if (stack.Count == 0)
                {
                    foreach (var workItem in proctor.PullWork())
                    {
                        stack.Push(workItem);
                    }
                }

                while (stack.Count != 0)
                {
                    proctor.SetStackRunning(threadId);
                    DepthFirstSearch(end, stack);
                    proctor.SetStackWaiting(threadId);
                    foreach (var workItem in proctor.PullWork())
                    {
                        stack.Push(workItem);
                    }
                }

                shouldTerminate = proctor.IsCompleted;
                Thread.Sleep(50);
            }

            Console.WriteLine("Terminating thread with ID: {0}", threadId);
        }

        private void AddPathToSolutionMap(CodePath path, int maxTrim = -1)
        {
            Method method = null;
            foreach (var subPath in path.GetSubPaths(maxTrim))
            {
                if (method != null)
                {
                    if (!pathSolutionMap.ContainsKey(method))
                    {
                        pathSolutionMap[method] = new List<CodePath>();
                    }

                    if (!pathSolutionMap[method].Contains(subPath))
                    {
                        pathSolutionMap[method].Add(subPath);
                    }
                    solutionMethods.Add(method);
                }

                method = subPath.FirstMethod;
            }
        }

        private void DepthFirstSearch(Method end, Stack<CodePath> stack)
        {
            while (stack.Count > 0)
            {
                var path = stack.Pop();
                var currentMethod = path.LastMethod;

                if (currentMethod.Caller != null)
                {
                    var map = this.nodeCompletionMap[currentMethod.Caller];
                    if (map.Set(currentMethod.CalleeIndex))
                    {
                        this.completedMethods.Add(currentMethod.Caller);
                        visited.Add(currentMethod.Caller);
                    }
                }

                // we have found the goal method!!
                // record the path we took to this point and
                // cache all sub-paths to this node for performance
                if (end == currentMethod)
                {
                    partialPaths.Add(path);
                    AddPathToSolutionMap(path);
                    continue;
                }

                if (solutionMethods.Contains(currentMethod))
                {
                    partialPaths.Add(path);
                    AddPathToSolutionMap(path);
                }

                if (visited.Contains(currentMethod))
                {
                    //if (!this.nodeCompletionMap.ContainsKey(currentMethod))
                    //{
                    //    var ohBully = 1;
                    //}

                    //if (!this.completedMethods.Contains(currentMethod) && 
                    //    !this.nodeCompletionMap[currentMethod].IsComplete)
                    //{
                    //    this.backlog.Add(path);
                    //}

                    continue;
                }

                var neighbors = this.analyzer.GetMethodNeighbors(currentMethod);

                this.nodeCompletionMap.GetOrAdd(currentMethod,
                    new MethodCompletionRing(currentMethod, neighbors.Count));

                var index = 0;
                foreach (var neighbor in neighbors)
                {
                    neighbor.CalleeIndex = index;
                    neighbor.Caller = currentMethod;

                    // check to prevent cycles
                    if (!path.Any(x => x == neighbor))
                    {
                        stack.Push(path.AddLastNew(neighbor));
                        index++;
                    }
                }
            }
        }
    }
}
