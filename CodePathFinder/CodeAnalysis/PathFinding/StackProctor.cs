namespace CodePathFinder.CodeAnalysis.PathFinding
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// Proctors a set of stacks for use with multiple threads
    /// </summary>
    public class StackProctor
    {
        /// <summary>
        /// The amount of work in one stack that is needed, at minimum, to give work over to another stack
        /// </summary>
        private const int workThreshold = 10;

        /// <summary>
        /// THe amount of work given in a stack to where half the work is given
        /// </summary>
        private const int halvingThreshold = 50;

        /// <summary>
        /// Number of times a thread is allowed to pull work before the operation fails
        /// </summary>
        private const int numRetry = 6;

        /// <summary>
        /// The number of stacks to proctor
        /// </summary>
        private readonly int numStacks;

        /// <summary>
        /// The actual stacks
        /// </summary>
        private Stack<CodePath>[] stacks;

        /// <summary>
        /// Specifies which stacks are in a waiting state
        /// </summary>
        private bool[] stackWaiting;

        /// <summary>
        /// Initializes a new instance of the <see cref="StackProctor" /> class
        /// </summary>
        /// <param name="numStacks">number of stacks to initialize</param>
        public StackProctor(int numStacks)
        {
            this.numStacks = numStacks;
            this.stacks = new Stack<CodePath>[numStacks];
            this.stackWaiting = new bool[numStacks];

            for (int i = 0; i < numStacks; i++)
            {
                this.stacks[i] = new Stack<CodePath>();
            }
        }
        
        /// <summary>
        /// Sets a stack's status as waiting
        /// </summary>
        /// <param name="stackId">ID of stack to set as waiting</param>
        public void SetStackWaiting(int stackId)
        {
            this.stackWaiting[stackId] = true;
        }

        /// <summary>
        /// Sets a stack's status as running
        /// </summary>
        /// <param name="stackId">ID of stack to set as running</param>
        public void SetStackRunning(int stackId)
        {
            this.stackWaiting[stackId] = false;
        }

        /// <summary>
        /// Looks for work in a stack until it finds it, up to <see cref="numRetry" /> times
        /// </summary>
        /// <returns>enumerable of work</returns>
        public IEnumerable<CodePath> PullWork()
        {
            for (int i = 0; i < numRetry; i++)
            {
                var stackIndex = i % numStacks;
                var stack = this.stacks[stackIndex];
                if (stack.Count >= workThreshold)
                {
                    lock (stack)
                    {
                        if (stack.Count < halvingThreshold)
                        {
                            for (int y = 0; y < workThreshold / 2; y++)
                            {
                                yield return stack.Pop();
                            }
                        }
                        else
                        {
                            for (int y = 0; y < stack.Count / 2; y++)
                            {
                                yield return stack.Pop();
                            }
                        }
                    }

                    yield break;
                }

                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Gets a stack of the specified index
        /// </summary>
        /// <param name="index">the stack index</param>
        /// <returns>the stack at that position</returns>
        public Stack<CodePath> GetStack(int index)
        {
            return this.stacks[index];
        }

        /// <summary>
        /// Are all stacks empty and non-waiting
        /// </summary>
        public bool IsCompleted
        {
            get
            {
                var allWaiting = stackWaiting.All(x => x);
                return allWaiting && stacks.All(x => x.Count == 0);
            }
        }
    }
}
