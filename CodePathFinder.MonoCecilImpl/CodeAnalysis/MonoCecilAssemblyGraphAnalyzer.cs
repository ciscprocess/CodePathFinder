namespace CodePathFinder.MonoCecilImpl
{
    using CodeAnalysis;
    using CodePathFinder.CodeAnalysis;
    using Mono.Cecil;
    using Mono.Cecil.Cil;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Utility;

    /// <summary>
    /// Uses Mono.Cecil to analyze assemblies
    /// </summary>
    public class MonoCecilAssemblyGraphAnalyzer : IAssemblyGraphAnalyzer
    {
        /// <summary>
        /// All assemblies to search
        /// </summary>
        private readonly ISet<AssemblyDefinition> assemblies;

        /// <summary>
        /// List of assemblies (for performance)
        /// </summary>
        private readonly List<AssemblyDefinition> assemblyList;

        /// <summary>
        /// Cache of assembly names
        /// </summary>
        private readonly HashSet<string> assemblyNameCache;

        /// <summary>
        /// Maintains a mapping of base type to implementation (for performance)
        /// </summary>
        private readonly ConcurrentDictionary<string, List<TypeDefinition>> typeImplementationCache;

        /// <summary>
        /// Utility for parsing type defs
        /// </summary>
        private readonly ITypeDefinitionUtility typeDefUtility;

        /// <summary>
        /// Assemblies split into partitions for thread consumption
        /// </summary>
        private readonly IList<AssemblyDefinition>[] splitAssemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoCecilAssemblyGraphAnalyzer" /> class
        /// </summary>
        /// <param name="assemblyResolver">the assembly resolver class to use</param>
        public MonoCecilAssemblyGraphAnalyzer(ISet<AssemblyDefinition> assemblies, 
            ITypeDefinitionUtility typeDefUtility, 
            int partitionFactor = 4)
        {
            this.assemblies = assemblies;
            this.assemblyNameCache = new HashSet<string>(this.assemblies.Select(x => x.FullName));
            this.assemblyList = this.assemblies.ToList();
            this.splitAssemblies = new IList<AssemblyDefinition>[partitionFactor];

            for (int i = 0; i < partitionFactor; i++)
            {
                this.splitAssemblies[i] = new List<AssemblyDefinition>();
            }

            for (int i = 0; i < this.assemblies.Count; i++)
            {
                var index = i % partitionFactor;
                this.splitAssemblies[index].Add(this.assemblies.ElementAt(i));
            }

            this.typeImplementationCache = new ConcurrentDictionary<string, List<TypeDefinition>>();
            this.typeDefUtility = typeDefUtility;
        }

        /// <summary>
        /// Gets the other <see cref="MonoCecilMethod" /> objects which this method calls in its body
        /// </summary>
        /// <param name="method">the method to search</param>
        /// <returns>found neighbors</returns>
        public IList<Method> GetMethodNeighbors(Method method)
        {
            if (method == null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            var monoCecilMethod = (MonoCecilMethod)method;
            var ilDefinition = monoCecilMethod.IlDefinition;

            if (ilDefinition == null ||
                (ilDefinition.Body == null && !ilDefinition.IsAbstract))
            {
                return new List<Method>();
            }

            if (ilDefinition.IsAbstract)
            {
                var allMethods = new List<Method>();
                var realMethods = ResolveAbstractToImplementations(monoCecilMethod);
                var found = false;
                foreach (var realMethod in realMethods)
                {
                    if (realMethod != null &&
                        realMethod.IlDefinition != null &&
                        !realMethod.IlDefinition.IsAbstract)
                    {
                        allMethods.Add(realMethod);
                        found = true;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("Warning: No implementation found for: \n\t\"{0}\"", method.FullName);
                }

                return allMethods;
            }

            var calls = new List<Method>();
            if (ilDefinition == null || ilDefinition.Body == null)
            {
                return calls;
            }

            Parallel.ForEach(ilDefinition.Body.Instructions,
                (instruction, state, index) => 
                {
                    try
                    {
                        if (instruction.OpCode == OpCodes.Call ||
                            instruction.OpCode == OpCodes.Callvirt ||
                            instruction.OpCode == OpCodes.Calli)
                        {
                            var reference = ResolveMethodReference(instruction.Operand);

                            if (reference != null &&
                                this.assemblyNameCache.Contains(reference.Module.Assembly.FullName))
                            {
                                lock (calls)
                                {
                                    calls.Add(new MonoCecilMethod(reference));
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("wowww.... {0}", ex.ToString());
                    }

                });

            return calls;
        }

        /// <summary>
        /// Resolves an abstract method (I.E. on an interface or abstract class) to all found implementations
        /// </summary>
        /// <param name="abstractMethod">the method to resolve</param>
        /// <returns>the found implementations</returns>
        private IEnumerable<MonoCecilMethod> ResolveAbstractToImplementations(MonoCecilMethod abstractMethod)
        {
            var def = abstractMethod.IlDefinition;
            var absType = def.DeclaringType;
            if (!absType.IsInterface && !absType.IsAbstract)
            {
                yield return abstractMethod;
                yield break;
            }

            List<TypeDefinition> allTypes;
            if (!typeImplementationCache.TryGetValue(absType.FullName, out allTypes))
            {
                allTypes = new List<TypeDefinition>();

                var tasks = this.splitAssemblies
                    .Select(x => Task.Run(() => GetImplementingTypes(absType, x)))
                    .ToArray();

                Task.WaitAll(tasks);

                foreach (var t in tasks)
                {
                    allTypes.AddRange(t.Result);
                }

                typeImplementationCache.GetOrAdd(absType.FullName, allTypes);
            }

            for (int i = 0; i < allTypes.Count; i++)
            {
                var type = allTypes[i];
                for (int j = 0; j < type.Methods.Count; j++)
                {
                    var method = new MonoCecilMethod(type.Methods[j]);
                    if (method.DoSignaturesMatch(abstractMethod))
                    {
                        yield return method;
                    }
                }
            }
        }

        private IList<TypeDefinition> GetImplementingTypes(TypeDefinition absType, 
            IList<AssemblyDefinition> partitionedAssemblies)
        {
            var allTypes = new List<TypeDefinition>();
            for (int i = 0; i < partitionedAssemblies.Count; i++)
            {
                var types = partitionedAssemblies[i].MainModule.Types;
                for (int j = 0; j < types.Count; j++)
                {
                    var type = types[j];
                    if (type.DoesSpecificTypeImplementInterface(absType) ||
                        typeDefUtility.IsSubclassOf(type, absType))
                    {
                        allTypes.Add(type);
                    }
                }
            }

            return allTypes;
        }

        /// <summary>
        /// Resolves a method reference if needed (is this actually needed?)
        /// </summary>
        /// <param name="reference">method or method reference to resolve</param>
        /// <returns>the resolved method</returns>
        private static MethodDefinition ResolveMethodReference(object reference)
        {
            if (reference is MethodReference)
            {
                try
                {
                    return (reference as MethodReference).Resolve();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Reference Error: {1}\n {0}", ex.ToString(), ((MethodReference)reference).FullName);
                }
            }
            else if (reference is MethodDefinition)
            {
                return (MethodDefinition)reference;
            }

            throw new InvalidOperationException("Can't resolve method");
        }
    }
}
