namespace CodePathFinder.CodeAnalysis
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Loader for domain assemblies
    /// </summary>
    /// <typeparam name="T">the type of assembly</typeparam>
    /// <typeparam name="K">the type of method</typeparam>
    public interface IAssemblyLoader<T, K> where
        K : Method
    {
        /// <summary>
        /// Loads assemblies by specified options
        /// </summary>
        /// <param name="options">match asssemblies to load by metadata. matches all if empty</param>
        /// <param name="progressHandler">progress handler</param>
        /// <returns>set of matching assemblies</returns>
        ISet<T> LoadDomainAssemblies(AssemblyMetadataOption[] options,
            IProgress<AssemblyLoadProgressUpdate> progressHandler = null);

        /// <summary>
        /// Load all methods that contain the substring
        /// </summary>
        /// <param name="fullNameContains">the substring to search by</param>
        /// <returns>all methods</returns>
        ISet<K> LoadMethods(string fullNameContains);

        /// <summary>
        /// Returns the method that matches the given full name
        /// </summary>
        /// <param name="fullName">the full name to match</param>
        /// <returns>the found method</returns>
        K LoadMethod(string fullName);
    }
}
