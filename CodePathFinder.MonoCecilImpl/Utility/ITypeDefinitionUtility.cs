namespace CodePathFinder.MonoCecilImpl.Utility
{
    using System.Collections.Generic;
    using Mono.Cecil;

    /// <summary>
    /// Providies utility methods for <see cref="TypeDefinition" /> objects
    /// </summary>
    public interface ITypeDefinitionUtility
    {
        /// <summary>
        /// Checks if childTypeDef a subclass of parentTypeDef. Does not test interface inheritance
        /// </summary>
        /// <param name="child">child class to check</param>
        /// <param name="parent">parent class to check</param>
        /// <returns>true if child is a subclass of parent</returns>
        bool IsSubclassOf(TypeDefinition child, TypeDefinition parent);
    }
}