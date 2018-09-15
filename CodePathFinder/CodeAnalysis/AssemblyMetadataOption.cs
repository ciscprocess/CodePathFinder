namespace CodePathFinder.CodeAnalysis
{
    /// <summary>
    /// Options for filtering metadata
    /// </summary>
    public class AssemblyMetadataOption
    {
        /// <summary>
        /// Different types to filter by
        /// </summary>
        public enum AssemblyMetadataAttribute
        {
            FileName,
            CompanyName,
            ProductName,
            LegalCopyright
        }

        /// <summary>
        /// Default value (clunky)
        /// </summary>
        private const string DefaultValue = "\0\0\0\0\0";

        /// <summary>
        /// What attribute to filter by
        /// </summary>
        public AssemblyMetadataAttribute AttributeType { get; set; } = AssemblyMetadataAttribute.FileName;

        /// <summary>
        /// Value to filter by
        /// </summary>
        public string Value { get; set; } = DefaultValue;

        /// <summary>
        /// If true, exclude from results rather than include
        /// </summary>
        public bool Exclude { get; set; } = false;

        /// <summary>
        /// If true, the <see cref="Value" /> matching field uses Regex
        /// </summary>
        public bool IsRegex { get; set; } = false;
    }
}
