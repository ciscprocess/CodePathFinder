namespace CodePathFinder.MonoCecilImpl.CodeAnalysis
{
    using Mono.Cecil;

    /// <summary>
    /// Extension methods for the <see cref="TypeDefinition" /> class
    /// </summary>
    static internal class TypeDefinitionExtensions
    {
        /// <summary>
        /// Does the childType directly inherit from parentInterface. Base
        /// classes of childType are not tested
        /// </summary>
        /// <param name="child">child type</param>
        /// <param name="parent">parent type</param>
        /// <returns>true if type implements other</returns>
        public static bool DoesSpecificTypeImplementInterface(this TypeDefinition child, TypeDefinition parent)
        {
            for (int i = 0; i < child.Interfaces.Count; i++)
            {
                var iface = child.Interfaces[i];
                if (DoesSpecificInterfaceImplementInterface(iface.InterfaceType, parent))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if interfaces match
        /// </summary>
        /// <param name="interface1">the first interface</param>
        /// <param name="interface2">the second interface</param>
        /// <returns>true on interface match</returns>
        public static bool DoesSpecificInterfaceImplementInterface(TypeReference interface1, 
            TypeDefinition interface2)
        {
            return interface1.FullName == interface2.FullName;
        }
    }
}
