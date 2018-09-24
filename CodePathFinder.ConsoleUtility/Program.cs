namespace CodePathFinder.ConsoleUtility
{
    using CodeAnalysis;
    using CodeAnalysis.PathFinding;
    using CodePathFinder.CodeAnalysis.Logging;
    using CodePathFinder.ConsoleUtility.Logging;
    using MonoCecilImpl;
    using MonoCecilImpl.CodeAnalysis;
    using MonoCecilImpl.Utility;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// Holds the entry point for console Code Path Finder
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Default path to domain assemblies
        /// </summary>
        private const string DefaultAsmPath =
            @"C:\Users\Nathan\Desktop\bin";

        /// <summary>
        /// Default "include" options when loading assemblies
        /// </summary>
        private static readonly AssemblyMetadataOption[] options = new AssemblyMetadataOption[2]
            {
                new AssemblyMetadataOption()
                {
                    AttributeType = AssemblyMetadataOption.AssemblyMetadataAttribute.ProductName,
                    Exclude = false,
                    IsRegex = true,
                    Value = ".*airwatch.*"
                },
                new AssemblyMetadataOption()
                {
                    AttributeType = AssemblyMetadataOption.AssemblyMetadataAttribute.LegalCopyright,
                    Exclude = false,
                    IsRegex = true,
                    Value = ".*airwatch.*"
                }
            };

        /// <summary>
        /// Entry point for console Code Path Finder
        /// </summary>
        static void Main(string[] args)
        {
            AppLogger.RegisterLogger(new ConsoleLogger());

            var assemblyLoader = new MonoCecilAssemblyLoader(DefaultAsmPath);
            var assemblies = assemblyLoader.LoadDomainAssemblies(options);

            var startMethod = assemblyLoader.LoadMethods(
                "WanderingWiFi.AirWatch.DeviceServices.Handlers.SecureChannelEndPointHandler.ProcessRequest")
                .First();

            var endMethod = assemblyLoader.LoadMethods(
                "WanderingWiFi.AirWatch.Entity.Certificate.CertificateLoad._GenerateCertificate")
                .First();

            var asmGraphAnalyzer = new MonoCecilAssemblyGraphAnalyzer(assemblies, new TypeDefinitionUtility());
            var pathFinder = new DepthFirstCodePathFinder(asmGraphAnalyzer);
            var allPaths = pathFinder.EnumeratePathsBetweenMethods(startMethod, 
                endMethod, 
                default(CancellationToken)).Result;

            var batchSize = 10000;
            using (var fs = new FileStream(@"D:\results.txt", FileMode.Create, FileAccess.ReadWrite))
            using (var writer = new StreamWriter(fs))
            {
                var batch = new List<CodePath>();
                foreach (var path in allPaths)
                {
                    batch.Add(path);

                    if (batch.Count == batchSize)
                    {
                        foreach (var printPath in batch)
                        {
                            writer.WriteLine(printPath);
                        }

                        batch.Clear();
                        writer.Flush();
                    }
                }

                foreach (var printPath in batch)
                {
                    writer.WriteLine(printPath);
                }

                Console.ReadKey();
            }
        }
    }
}
