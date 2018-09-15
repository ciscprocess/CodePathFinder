namespace CodePathFinder.MonoCecilImpl
{
    using System;
    using Mono.Cecil;
    using CodePathFinder.CodeAnalysis;
    using System.Text.RegularExpressions;
    using System.Text;

    /// <summary>
    /// Represents a method in the search
    /// </summary>
    public class MonoCecilMethod : Method
    {
        /// <summary>
        /// Regex used to format the method for ToString()
        /// </summary>
        private static Regex AbbreviationRegex = new Regex(@".*\.([A-Za-z_]\w*)::([A-Za-z_]\w*)\(");

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoCecilMethod" /> class
        /// </summary>
        /// <param name="ilDefinition"></param>
        public MonoCecilMethod(MethodDefinition ilDefinition)
        {
            if (ilDefinition == null)
            {
                throw new ArgumentNullException(nameof(ilDefinition));
            }

            this.IlDefinition = ilDefinition;
        }

        /// <summary>
        /// Gets the fully-qualified name of the method, including:
        /// + Namespace
        /// + Parameters
        /// + Method Name
        /// + Return type
        /// </summary>
        public override string FullName
        {
            get
            {
                return this.IlDefinition.FullName;
            }
        }

        /// <summary>
        /// The IL definition for this Method
        /// </summary>
        public MethodDefinition IlDefinition { get; }

        /// <summary>
        /// Checks if the signature (return type, name, parameters) equals that of another
        /// </summary>
        /// <param name="other">the other whose sig to check</param>
        /// <returns>true on match; false otherwise</returns>
        public override bool DoSignaturesMatch(Method other)
        {
            if (!(other is MonoCecilMethod))
            {
                return false;
            }

            var def1 = this.IlDefinition;
            var def2 = ((MonoCecilMethod)other).IlDefinition;

            if (def1.Name != def2.Name ||
                def1.Parameters.Count != def2.Parameters.Count ||
                def1.ReturnType.FullName != def2.ReturnType.FullName)
            {
                return false;
            }

            for (int i = 0; i < def1.Parameters.Count; i++)
            {
                if (def1.Parameters[i].ParameterType.FullName != def2.Parameters[i].ParameterType.FullName)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks for equality
        /// </summary>
        /// <param name="otherMethod">the other object to check against</param>
        /// <returns>true if equal, false otherwise</returns>
        protected override bool EqualsImpl(object otherMethod)
        {
            return this.IlDefinition.FullName
                == ((MonoCecilMethod)otherMethod).IlDefinition.FullName;
        }

        /// <summary>
        /// Gets the hashcode for this method
        /// </summary>
        /// <returns>calculated hash code</returns>
        protected override int GetHashCodeImpl()
        {
            return IlDefinition.FullName.GetHashCode();
        }

        /// <summary>
        /// Represents the method in a string of form:
        /// [ClassName].[MethodName]
        /// </summary>
        /// <returns>the formatted string</returns>
        public override string ToString()
        {
            var match = AbbreviationRegex.Match(this.FullName);
            if (match.Success && match.Groups.Count > 2)
            {
                var builder = new StringBuilder();
                builder.AppendFormat("{0}.{1}(", match.Groups[1], match.Groups[2]);

                var paramCount = this.IlDefinition.Parameters.Count;
                for (int i = 0; i < paramCount - 1; i++)
                {
                    var parameter = this.IlDefinition.Parameters[i];
                    builder.AppendFormat("{0},", parameter.ParameterType.Name);
                }

                if (paramCount > 0)
                {
                    builder.Append(this.IlDefinition.Parameters[paramCount - 1].ParameterType.Name);
                }

                builder.Append(')');
                return builder.ToString();
            }

            return this.FullName;
        }
    }
}
