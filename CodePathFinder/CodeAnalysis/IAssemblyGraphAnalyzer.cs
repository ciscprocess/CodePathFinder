namespace CodePathFinder.CodeAnalysis
{
    using System.Collections.Generic;

    /// <summary>
    /// Analyzes assemblies
    /// </summary>
    public interface IAssemblyGraphAnalyzer
    {
        /// <summary>
        /// Gets the other <see cref="Method" /> objects which this method calls in its body
        /// </summary>
        /// <param name="method">the method to search</param>
        /// <returns>found neighbors</returns>
        IList<Method> GetMethodNeighbors(Method methods);
    }
}
