namespace CodePathFinder.CodeAnalysis.PathFinding
{
    using System.Collections.Generic;

    /// <summary>
    /// Specifies contract for finding a code path between methods
    /// </summary>
    public interface ICodePathFinder
    {
        /// <summary>
        /// Find a code call path between two different methods
        /// </summary>
        /// <param name="start">the starting method</param>
        /// <param name="end">the ending method</param>
        /// <param name="maxPathLength">the max length of all paths to find</param>
        /// <returns>all code paths</returns>
        IList<CodePath> FindPathsBetweenMethods(Method start, Method end, int maxPathLength = -1);
    }
}
