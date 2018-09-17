namespace CodePathFinder.MonoCecilImpl.CodeAnalysis
{
    using CodePathFinder.CodeAnalysis;
    using Mono.Cecil;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Loads Mono.Cecil assemblies
    /// </summary>
    public class MonoCecilAssemblyLoader : IAssemblyLoader<AssemblyDefinition, MonoCecilMethod>
    {
        /// <summary>
        /// Regex to retrieve type name
        /// </summary>
        private static Regex GetTypeRegex = new Regex(@"[A-Za-z_][\w.]* ([\w.]*)::.*", RegexOptions.Compiled);

        /// <summary>
        /// Path to search for assmblies in
        /// </summary>
        private readonly string assemblyFolderPath;

        /// <summary>
        /// All assemblies set
        /// </summary>
        private ISet<AssemblyDefinition> allAssemblies;

        /// <summary>
        /// The assembly resolver
        /// </summary>
        private readonly BaseAssemblyResolver assemblyResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoCecilAssemblyLoader" /> class 
        /// </summary>
        /// <param name="assemblyFolderPath">the path to search assemblies in</param>
        public MonoCecilAssemblyLoader(string assemblyFolderPath)
        {
            this.assemblyFolderPath = assemblyFolderPath;
            this.assemblyResolver = new DefaultAssemblyResolver();
            this.assemblyResolver.AddSearchDirectory(this.assemblyFolderPath);
            this.allAssemblies = new HashSet<AssemblyDefinition>();
        }

        /// <summary>
        /// Loads assemblies by specified options
        /// </summary>
        /// <param name="options">match asssemblies to load by metadata. matches all if empty</param>
        /// <param name="progressHandler">progress handler</param>
        /// <returns>set of matching assemblies</returns>
        public ISet<AssemblyDefinition> LoadDomainAssemblies(AssemblyMetadataOption[] options, 
            IProgress<AssemblyLoadProgressUpdate> progressHandler = null)
        {
            var dllFiles = Directory.EnumerateFiles(assemblyFolderPath, "*.dll");
            var exeFiles = Directory.EnumerateFiles(assemblyFolderPath, "*.exe");
            var asmFiles = dllFiles.Concat(exeFiles).ToList();

            for (int i = 0; i < asmFiles.Count; i++)
            {
                var asmFile = asmFiles[i];

                if (progressHandler != null)
                {
                    progressHandler.Report(new AssemblyLoadProgressUpdate()
                    {
                        CurrentAssemblyName = asmFile.Split('\\').Last(),
                        CurrentAssemblyNumber = i,
                        TotalAssemblies = asmFiles.Count
                    });
                }

                if (DoesConformToFilters(asmFile, options))
                {
                    var asm = AssemblyDefinition.ReadAssembly(asmFile,
                        new ReaderParameters { AssemblyResolver = this.assemblyResolver });

                    this.allAssemblies.Add(asm);
                }
            }

            return this.allAssemblies;
        }

        /// <summary>
        /// Load all methods that contain the substring
        /// </summary>
        /// <param name="fullNameContains">the substring to search by</param>
        /// <returns>all methods</returns>
        public ISet<MonoCecilMethod> LoadMethods(string fullNameContains)
        {
            var set = new HashSet<MonoCecilMethod>();
            var startMethodNameParts = fullNameContains.Split('.');
            var startMethodType = string.Join(".", startMethodNameParts.Take(startMethodNameParts.Length - 1).ToArray());
            var startMethodName = startMethodNameParts.Last();

            foreach (var assembly in this.allAssemblies)
            {
                var type = assembly.MainModule.GetType(startMethodType);
                if (type == null)
                {
                    continue;
                }

                var matches = type.Methods.Where(x => x.Name == startMethodName);

                foreach (var match in matches)
                {
                    set.Add(new MonoCecilMethod(match));
                }
            }

            return set;
        }

        /// <summary>
        /// Returns the method that matches the given full name
        /// </summary>
        /// <param name="fullName">the full name to match</param>
        /// <returns>the found method</returns>
        public MonoCecilMethod LoadMethod(string fullName)
        {
            var match = GetTypeRegex.Match(fullName);
            var typeName = match.Groups[1].Value;
            foreach (var assembly in allAssemblies)
            {
                var type = assembly.MainModule.GetType(typeName);
                if (type != null)
                {
                    foreach (var method in type.Methods)
                    {
                        if (method.FullName == fullName)
                        {
                            return new MonoCecilMethod(method);
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Does the file conform to assembly options
        /// </summary>
        /// <param name="filePath">path of file to check</param>
        /// <param name="include">include any assemblies that match any parameter</param>
        /// <returns>true if file is conformant with filters</returns>
        private bool DoesConformToFilters(string filePath, 
            AssemblyMetadataOption[] options)
        {
            if (options == null || options.Length == 0)
            {
                return true;
            }

            var versionInfo = FileVersionInfo.GetVersionInfo(filePath);
            if (versionInfo == null && 
                options.Any(x => x.AttributeType != AssemblyMetadataOption.AssemblyMetadataAttribute.FileName))
            {
                return false;
            }

            foreach (var option in options)
            {
                var isMatch = false;
                switch (option.AttributeType)
                {
                    case AssemblyMetadataOption.AssemblyMetadataAttribute.CompanyName:
                        isMatch = DoesMatch(versionInfo.CompanyName, option);
                    break;
                    case AssemblyMetadataOption.AssemblyMetadataAttribute.FileName:
                        isMatch = DoesMatch(filePath, option);
                    break;
                    case AssemblyMetadataOption.AssemblyMetadataAttribute.LegalCopyright:
                        isMatch = DoesMatch(versionInfo.LegalCopyright, option);
                    break;
                    case AssemblyMetadataOption.AssemblyMetadataAttribute.ProductName:
                        isMatch = DoesMatch(versionInfo.ProductName, option);
                    break;
                    default:
                        throw new InvalidOperationException("Invalid filter type specified.");
                }

                if (option.Exclude && isMatch)
                {
                    return false;
                }
                else if (isMatch)
                {
                    return true;
                }
            }

            return false;
        }

        private bool DoesMatch(string value, AssemblyMetadataOption option)
        {
            if (option.IsRegex)
            {
                if (value == null)
                {
                    return false;
                }

                var regex = new Regex(option.Value, RegexOptions.IgnoreCase);
                return regex.IsMatch(value);
            }
            else
            {
                return option.Value == value;
            }
        }
    }
}
