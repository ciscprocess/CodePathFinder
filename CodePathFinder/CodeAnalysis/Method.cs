namespace CodePathFinder.CodeAnalysis
{
    /// <summary>
    /// Represents a base method
    /// </summary>
    public abstract class Method
    {
        /// <summary>
        /// The order in callers body that this method lies
        /// </summary>
        public virtual int CalleeIndex { get; set; }

        /// <summary>
        /// A method that calls this method, if applicable
        /// </summary>
        public virtual Method Caller { get; set; }

        /// <summary>
        /// Gets the fully-qualified name of the method, including:
        /// + Namespace
        /// + Parameters
        /// + Method Name
        /// + Return type
        /// </summary>
        public abstract string FullName { get; }

        /// <summary>
        /// Checks if the signature (return type, name, parameters) equals that of another
        /// </summary>
        /// <param name="other">the other whose sig to check</param>
        /// <returns>true on match; false otherwise</returns>
        public abstract bool DoSignaturesMatch(Method other);

        /// <summary>
        /// Checks for equality
        /// </summary>
        /// <param name="otherMethod">the other object to check against</param>
        /// <returns>true if equal, false otherwise</returns>
        public override bool Equals(object otherMethod)
        {
            if (otherMethod == (object)this)
            {
                return true;
            }

            if (otherMethod == null || (object)this == null)
            {
                return false;
            }

            return this.EqualsImpl(otherMethod);
        }

        /// <summary>
        /// Gets the hashcode for this method
        /// </summary>
        /// <returns>calculated hash code</returns>
        public override int GetHashCode()
        {
            return this.GetHashCodeImpl();
        }

        /// <summary>
        /// Internal implementation for <see cref="GetHashCode"/>
        /// </summary>
        /// <returns>integer</returns>
        protected abstract int GetHashCodeImpl();

        /// <summary>
        /// Internal implementation for <see cref="Equals(object)"/>
        /// </summary>
        /// <param name="otherMethod">other method</param>
        /// <returns>boolean</returns>
        protected abstract bool EqualsImpl(object otherMethod);

        /// <summary>
        /// Equality overload. Simply calls Equals
        /// </summary>
        /// <param name="this">the left hand arg</param>
        /// <param name="other">the right hand arg</param>
        /// <returns>true if equal</returns>
        public static bool operator ==(Method @this, Method other)
        {
            if (other == (object)@this)
            {
                return true;
            }

            if (other == null || (object)@this == null)
            {
                return false;
            }

            return @this.Equals(other);
        }

        /// <summary>
        /// Inequality overload. Simply calls Equals and negates
        /// </summary>
        /// <param name="this">the left hand arg</param>
        /// <param name="other">the right hand arg</param>
        /// <returns>false if equal</returns>
        public static bool operator !=(Method @this, Method other)
        {
            if ((object)@this == null)
            {
                if ((object)other == null)
                {
                    return false;
                }

                return true;
            }

            return !@this.Equals(other);
        }
    }
}
