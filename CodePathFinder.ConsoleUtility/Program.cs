namespace CodePathFinder.ConsoleUtility
{
    using CodeAnalysis;
    using CodeAnalysis.PathFinding;
    using MonoCecilImpl;
    using MonoCecilImpl.CodeAnalysis;
    using MonoCecilImpl.Utility;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Holds the entry point for console Code Path Finder
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Default path to domain assemblies
        /// </summary>
        private const string DefaultAsmPath =
            @"C:\Windows\Microsoft.NET\assembly\GAC_64\System.Web\v4.0_4.0.0.0__b03f5f7f11d50a3a";

        /// <summary>
        /// Default "include" options when loading assemblies
        /// </summary>
        private static readonly AssemblyMetadataOption[] options = new AssemblyMetadataOption[0];

        /// <summary>
        /// Entry point for console Code Path Finder
        /// </summary>
        static void Main(string[] args)
        {
            var assemblyLoader = new MonoCecilAssemblyLoader(DefaultAsmPath);
            var assemblies = assemblyLoader.LoadDomainAssemblies(options);

            var startMethod = assemblyLoader.LoadMethods(
                args[0])
                .First();

            var endMethod = assemblyLoader.LoadMethods(
                args[1])
                .First();

            var asmGraphAnalyzer = new MonoCecilAssemblyGraphAnalyzer(assemblies, new TypeDefinitionUtility());
            var pathFinder = new DepthFirstCodePathFinder(asmGraphAnalyzer);
            var allPaths = pathFinder.FindPathsBetweenMethods(startMethod, endMethod);

            File.WriteAllLines("results.txt", allPaths.Select(x => x.ToString()).ToArray());
        }
    }
}
