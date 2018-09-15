namespace CodePathFinder.MonoCecilImpl.Utility
{
    using Mono.Cecil;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    /// <summary>
    /// Providies utility methods for <see cref="TypeDefinition" /> objects
    /// </summary>
    public class TypeDefinitionUtility : ITypeDefinitionUtility
    {
        /// <summary>
        /// Cache of base types
        /// </summary>
        private readonly ConcurrentDictionary<string, TypeDefinition> baseTypeMap = 
            new ConcurrentDictionary<string, TypeDefinition>();

        /// <summary>
        /// All class names where, when they're encountered, stop the operation
        /// </summary>
        private readonly ISet<string> classNameBlacklist;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeDefinitionUtility" /> class
        /// </summary>
        /// <param name="classNameBlackList">class names to stop processing</param>
        public TypeDefinitionUtility(ISet<string> classNameBlackList = null)
        {
            this.classNameBlacklist = classNameBlacklist ??
                new HashSet<string>(new string[] { "System.Object" });
        }

        /// <summary>
        /// Checks if childTypeDef a subclass of parentTypeDef. Does not test interface inheritance
        /// </summary>
        /// <param name="child">child class to check</param>
        /// <param name="parent">parent class to check</param>
        /// <returns>true if child is a subclass of parent</returns>
        public bool IsSubclassOf(TypeDefinition child, TypeDefinition parent)
        {
            for (var baseClass = child; baseClass != null; baseClass = GetBaseType(baseClass))
            {
                if (baseClass.FullName == parent.FullName)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets teh base type for a given class
        /// </summary>
        /// <param name="def">the base type</param>
        /// <returns>the base type</returns>
        private TypeDefinition GetBaseType(TypeDefinition def)
        {
            var fn = def.FullName;

            if (def.BaseType == null ||
                this.classNameBlacklist.Contains(fn) || 
                this.classNameBlacklist.Contains(def.BaseType.FullName))
            {
                return null;
            }

            TypeDefinition gotType = null;
            if (baseTypeMap.TryGetValue(fn, out gotType))
            {
                return gotType;
            }
            else
            {
                return baseTypeMap.GetOrAdd(fn, def.BaseType?.Resolve());
            }
        }
    }
}
