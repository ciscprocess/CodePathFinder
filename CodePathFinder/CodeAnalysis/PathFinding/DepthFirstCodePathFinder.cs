namespace CodePathFinder.CodeAnalysis.PathFinding
{
    using CodePathFinder.CodeAnalysis.Logging;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Single-threaded DFS based explore algorithm for finding code paths
    /// </summary>
    public class DepthFirstCodePathFinder : ICodePathFinder
    {
        /// <summary>
        /// The assembly analyzer to use
        /// </summary>
        private readonly IAssemblyGraphAnalyzer analyzer;

        /// <summary>
        /// The the partially formed paths
        /// </summary>
        private List<CodePath> partialPaths;

        /// <summary>
        /// Marks if a method has been visited
        /// </summary>
        private HashSet<Method> visited;

        /// <summary>
        /// If a method ultimately has one or more paths to the solution method,
        /// they are cached here to speed up search and maintain correctness in a
        /// multi-threaded environment
        /// </summary>
        private Dictionary<Method, List<CodePath>> pathSolutionMap;

        /// <summary>
        /// Like <see cref="pathSolutionMap" /> but only a set (only the keys)
        /// </summary>
        private HashSet<Method> solutionMethods;

        /// <summary>
        /// Specifies which methods have had all their children explored and can 
        /// be considered "completed"
        /// </summary>
        private HashSet<Method> completedMethods;


        /// <summary>
        /// Initializes a new instance of the <see cref="DepthFirstCodePathFinder" /> class
        /// </summary>
        /// <param name="analyzer">The assembly analyzer to use</param>
        /// <param name="numThreads">the number of search threads</param>
        public DepthFirstCodePathFinder(IAssemblyGraphAnalyzer analyzer)
        {
            this.analyzer = analyzer;
        }

        /// <summary>
        /// Find all paths between two methods
        /// </summary>
        /// <param name="start">method to start from</param>
        /// <param name="end">goal method</param>
        /// <param name="maxPathLength">the upper limit of path size; exclude paths longer than this</param>
        /// <returns>all paths</returns>
        public async Task<IList<CodePath>> FindPathsBetweenMethods(Method start, 
            Method end,
            CancellationToken cancellationToken,
            int maxPathLength = -1)
        {
            await ConstructPartialPaths(start, end, cancellationToken);
            return ConstructFullPaths(start, end, cancellationToken, maxPathLength).ToList();
        }

        /// <summary>
        /// Return an iterator that allows stepping through the results of
        /// finding all paths between two methods
        /// </summary>
        /// <param name="start">method to start from</param>
        /// <param name="end">goal method</param>
        /// <param name="maxPathLength">the upper limit of path size; exclude paths longer than this</param>
        /// <returns></returns>
        public async Task<IEnumerable<CodePath>> EnumeratePathsBetweenMethods(Method start, 
            Method end,
            CancellationToken cancellationToken,
            int maxPathLength = -1)
        {
            await ConstructPartialPaths(start, end, cancellationToken);
            return ConstructFullPaths(start, end, cancellationToken, maxPathLength);
        }

        /// <summary>
        /// Gets an internal enumerator for and completes the final pahts
        /// </summary>
        /// <param name="start">method to start from</param>
        /// <param name="end">goal method</param>
        /// <param name="maxPathLength">the upper limit of path size; exclude paths longer than this</param>
        /// <returns>enumerator/generator for paths</returns>
        public IEnumerable<CodePath> ConstructFullPaths(Method start, 
            Method end, 
            CancellationToken token, 
            int maxPathLength = -1)
        {
            var pathStack = new Stack<CodePath>(this.partialPaths);
            while (pathStack.Count > 0)
            {
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                    yield break;
                }

                var path = pathStack.Pop();
                if (path.LastMethod == end)
                {
                    if (maxPathLength > -1 && 
                        path.Length > maxPathLength)
                    {
                        continue;
                    }

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

        /// <summary>
        /// Constructs the partial paths which forms the sub-graph of solution nodes
        /// </summary>
        /// <param name="start">starting method</param>
        /// <param name="end">ending method</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>void Task</returns>
        public async Task ConstructPartialPaths(Method start, 
            Method end,
            CancellationToken cancellationToken)
        {
            this.partialPaths = new List<CodePath>();
            this.visited = new HashSet<Method>();
            this.pathSolutionMap = new Dictionary<Method, List<CodePath>>();
            this.solutionMethods = new HashSet<Method>();
            this.completedMethods = new HashSet<Method>();

            // left for potential future multi-threading support
            var proctor = new StackProctor(1);
            var stack = proctor.GetStack(0);
            stack.Push(CodePath.FromSingleMethod(start));

            await Task.Run(() => RunSearchThread(end, proctor, 0, cancellationToken));
        }

        /// <summary>
        /// Runs a search thread for parallel DFS.
        /// (here for potential multi-threading support in the future)
        /// </summary>
        /// <param name="end">goal method</param>
        /// <param name="proctor">the stack "proctor" for all threads</param>
        /// <param name="threadId">the ID for this thread (for use with proctor)</param>
        /// <param name="cancellationToken">the cancellation token</param>
        private void RunSearchThread(Method end, 
            StackProctor proctor, 
            int threadId,
            CancellationToken cancellationToken)
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
                    DepthFirstSearch(end, stack, cancellationToken);

                    if (cancellationToken.IsCancellationRequested)
                    {
                        AppLogger.Current.Debug(
                            "Thread with ID: {0} has had its operation cancelled. Terminating.",
                            threadId);

                        cancellationToken.ThrowIfCancellationRequested();

                        return;
                    }

                    proctor.SetStackWaiting(threadId);
                    foreach (var workItem in proctor.PullWork())
                    {
                        stack.Push(workItem);
                    }
                }

                shouldTerminate = proctor.IsCompleted;
                Thread.Sleep(50);
            }

            AppLogger.Current.Debug(
                "Terminating thread with ID: {0}", 
                threadId);
        }

        /// <summary>
        /// Runs a partial depth-first search on the method space
        /// </summary>
        /// <param name="end">goal method</param>
        /// <param name="stack">stack to use -- should be pre-seeded</param>
        /// <param name="cancellationToken">cancellation token</param>
        private void DepthFirstSearch(Method end, 
            Stack<CodePath> stack,
            CancellationToken cancellationToken)
        {
            while (stack.Count > 0)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    return;
                }

                var path = stack.Pop();
                var currentMethod = path.LastMethod;

                // we have found the goal method!!
                // record the path we took to this point and
                // cache all sub-paths to this node for performance
                if (end == currentMethod)
                {
                    partialPaths.Add(path);
                    AddPathToSolutionMap(path);
                    continue;
                }

                // we have reached a method that leads to
                // the goal method. record the path to
                // this point and then cache all subpaths
                if (solutionMethods.Contains(currentMethod))
                {
                    partialPaths.Add(path);
                    AddPathToSolutionMap(path);
                    continue;
                }

                if (visited.Contains(currentMethod))
                {
                    continue;
                }

                var neighbors = this.analyzer.GetMethodNeighbors(currentMethod);

                foreach (var neighbor in neighbors)
                {
                    neighbor.Caller = currentMethod;

                    // check to prevent cycles
                    if (!path.Any(x => x == neighbor))
                    {
                        stack.Push(path.AddLastNew(neighbor));
                    }
                }

                visited.Add(currentMethod);
            }
        }

        /// <summary>
        /// The <see cref="path" /> parameter must end in either a goal method,
        /// or a known intermediate method. For each subsequence ending in the same
        /// method, add the beginning node to known solution nodes
        /// </summary>
        /// <param name="path">the path, and whose subpaths, to cache</param>
        private void AddPathToSolutionMap(CodePath path)
        {
            Method method = null;
            foreach (var subPath in path.GetSubPaths())
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
    }
}
