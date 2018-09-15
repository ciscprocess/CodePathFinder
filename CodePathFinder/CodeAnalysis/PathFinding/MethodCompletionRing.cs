namespace CodePathFinder.CodeAnalysis.PathFinding
{
    /// <summary>
    /// Tracks how many of the child methods of a given <see cref="Method"/> object
    /// are visited
    /// </summary>
    public class MethodCompletionRing
    {
        /// <summary>
        /// The method to track
        /// </summary>
        private readonly Method method;

        /// <summary>
        /// Tracks the individual child method call states
        /// </summary>
        private readonly bool[] completionRing;

        /// <summary>
        /// The number of child methods
        /// </summary>
        private readonly int rungs;

        /// <summary>
        /// The number of times a method has been set from incomplete to complete
        /// NOTE: not thread-locked, so manual checks necessary
        /// </summary>
        private int completionSetCount = 0;

        /// <summary>
        /// Tracks whether this parent method has had all child methods called
        /// </summary>
        private bool isComplete = false;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodCompletionRing" /> class
        /// </summary>
        /// <param name="method">method to track</param>
        /// <param name="rungs">number of method calls (child methods)</param>
        public MethodCompletionRing(Method method, int rungs)
        {
            this.method = method;
            this.rungs = rungs;
            this.completionRing = new bool[this.rungs];
        }

        public int CompletedRungs
        {
            get
            {
                return this.completionSetCount;
            }
        }

        public int TotalRungs
        {
            get
            {
                return this.rungs;
            }
        }

        public bool IsComplete
        {
            get
            {
                return this.rungs == 0 || this.isComplete;
            }
        }

        public override string ToString()
        {
            return $"{this.completionSetCount} / {this.rungs}: {isComplete}";
        }

        public bool Set(int index)
        {
            if (rungs == 0)
            {
                return true;
            }

            completionSetCount += 1;
            this.completionRing[index] = true;

            if (!isComplete && completionSetCount >= rungs)
            {
                var temp = true;
                for (int i = 0; i < rungs; i++)
                {
                    if (!completionRing[i])
                    {
                        temp = false;
                    }
                }

                return isComplete = temp;
            }

            return isComplete;
        }
    }
}
