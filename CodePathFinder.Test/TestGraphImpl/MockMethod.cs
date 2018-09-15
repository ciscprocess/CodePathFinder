namespace CodePathFinder.Test.CodeAnalysis.PathFinding
{
    using CodePathFinder.CodeAnalysis;

    public class MockMethod : Method
    {
        /// <summary>
        /// ID of the method
        /// </summary>
        private int id;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockMethod" /> class
        /// </summary>
        /// <param name="id"></param>
        public MockMethod(int id)
        {
            this.id = id;
        }

        /// <summary>
        /// Full name of the mock (string id)
        /// </summary>
        public override string FullName
        {
            get { return this.id.ToString(); }
        }

        public override bool DoSignaturesMatch(Method other)
        {
            return ((MockMethod)other).id == this.id;
        }

        protected override bool EqualsImpl(object otherMethod)
        {
            return ((MockMethod)otherMethod).id == this.id;
        }

        protected override int GetHashCodeImpl()
        {
            return this.id;
        }

        public override string ToString()
        {
            return this.id.ToString();
        }
    }
}
