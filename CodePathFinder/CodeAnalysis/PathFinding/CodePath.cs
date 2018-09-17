namespace CodePathFinder.CodeAnalysis.PathFinding
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// A versioned linked-list implementation of a method call path
    /// </summary>
    public class CodePath : IEnumerable<Method>, IComparable<CodePath>
    {
        /// <summary>
        /// Tree path for nodes
        /// </summary>
        private Dictionary<ulong, int> treePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodePath" /> class
        /// </summary>
        public CodePath()
        {
            this.treePath = new Dictionary<ulong, int>();
        }

        /// <summary>
        /// Number of methods in the path
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// First method in the list
        /// </summary>
        public Method FirstMethod => FirstNode.Method;

        /// <summary>
        /// Last method in the list
        /// </summary>
        public Method LastMethod => LastNode.Method;

        /// <summary>
        /// First node in the code path
        /// </summary>
        private MethodNode FirstNode { get; set; }

        /// <summary>
        /// Last node in the code path
        /// </summary>
        private MethodNode LastNode { get; set; }

        /// <summary>
        /// Adds a method to the end of this code path
        /// </summary>
        /// <param name="node">method to add</param>
        public CodePath AddLast(Method method)
        {
            return AddLast(new MethodNode(method));
        }

        /// <summary>
        /// Adds a method to the end of this code path and returns new CodePath.
        /// Leaves the current instance unmodified
        /// </summary>
        /// <param name="node">method to add</param>
        /// <returns>cloned code path with new method added</returns>
        public CodePath AddLastNew(Method method)
        {
            if (this.FirstNode == null && this.LastNode == null)
            {
                return FromSingleMethod(method);
            }

            var @new = new CodePath();
            var newNode = new MethodNode(method);
            @new.FirstNode = this.FirstNode;
            var version = this.LastNode.AddNextNode(newNode);
            @new.treePath = new Dictionary<ulong, int>(this.treePath);
            @new.treePath[this.LastNode.NodeId] = version;

            @new.LastNode = newNode;
            @new.Length = this.Length + 1;

            return @new;
        }

        public CodePath Concat(CodePath other)
        {
            if (this.LastNode.NodeId == other.FirstNode.NodeId)
            {
                return this;
            }

            var version = this.LastNode.AddNextNode(other.FirstNode);
            this.treePath[this.LastNode.NodeId] = version;

            this.LastNode = other.LastNode;
            this.Length += other.Length;
            return this;
        }

        public CodePath ConcatNew(CodePath other)
        {
            if (this.LastNode.NodeId == other.FirstNode.NodeId)
            {
                return this;
            }

            var newPath = new CodePath();
            newPath.FirstNode = this.FirstNode;
            var version = this.LastNode.AddNextNode(other.FirstNode);
            newPath.treePath = new Dictionary<ulong, int>(this.treePath);

            foreach (var kv in other.treePath)
            {
                if (!newPath.treePath.ContainsKey(kv.Key))
                {
                    newPath.treePath[kv.Key] = kv.Value;
                }
            }

            newPath.treePath[this.LastNode.NodeId] = version;

            newPath.LastNode = other.LastNode;
            newPath.Length = this.Length + other.Length;
            return newPath;
        }

        /// <summary>
        /// Gets all sub paths like path[1..n], path[2..n], path[3..n] to path[n-1..n]
        /// </summary>
        /// <param name="maxTrim">maximum amount to trim by, inclusive</param>
        /// <returns>enumerable of sub-paths</returns>
        public IEnumerable<CodePath> GetSubPaths(int maxTrim = -1)
        {
            var current = this.GetNextNode(this.FirstNode);
            var trimmed = 1;
            while (current != null)
            {
                var newCodePath = new CodePath();
                newCodePath.FirstNode = current;
                newCodePath.LastNode = this.LastNode;
                newCodePath.treePath = new Dictionary<ulong, int>(this.treePath);
                newCodePath.Length = this.Length - trimmed;
                yield return newCodePath;

                if (current == this.LastNode)
                {
                    yield break;
                }

                current = this.GetNextNode(current);
                if (trimmed++ == maxTrim && maxTrim > 0)
                {
                    yield break;
                }
            }
        }

        /// <summary>
        /// Gets the typed enumerator for this class
        /// </summary>
        /// <returns>the typed enumerator</returns>
        public IEnumerator<Method> GetEnumerator()
        {
            return AllNodes.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator for this class
        /// </summary>
        /// <returns>the enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return AllNodes.GetEnumerator();
        }

        /// <summary>
        /// Compare the sort order for the two classes
        /// </summary>
        /// <param name="other">the class to compare to </param>
        /// <returns>0 for equal, -1 for other greater, 1 for this greater</returns>
        public int CompareTo(CodePath other)
        {
            var stringKeyBuilder1 = new StringBuilder();
            var stringKeyBuilder2 = new StringBuilder();

            foreach (var method in this)
            {
                stringKeyBuilder1.Append(method.FullName);
            }

            foreach (var method in other)
            {
                stringKeyBuilder2.Append(method.FullName);
            }

            var stringKey1 = stringKeyBuilder1.ToString();
            var stringKey2 = stringKeyBuilder2.ToString();

            return stringKey1.CompareTo(stringKey2);
        }

        public override bool Equals(object obj)
        {
            var other = (CodePath)obj;

            if (this.Length != other.Length)
            {
                return false;
            }

            var currentThis = this.FirstNode;
            var currentOther = other.FirstNode;

            while (currentThis != null && currentOther != null)
            {
                if (!currentThis.Identifier.Equals(currentOther.Identifier))
                {
                    return false;
                }

                currentThis = this.GetNextNode(currentThis);
                currentOther = other.GetNextNode(currentOther);
            }

            return true;
        }

        /// <summary>
        /// Returns all methods in this path, separated by newline
        /// </summary>
        /// <returns>the formatted string</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach (var method in this)
            {
                builder.AppendLine(method.FullName);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Creates a code path with a single method
        /// </summary>
        /// <param name="method">method to add</param>
        /// <returns>initialized code path</returns>
        public static CodePath FromSingleMethod(Method method)
        {
            var node = new MethodNode(method);
            var path = new CodePath
            {
                FirstNode = node,
                LastNode = node
            };

            path.Length = 1;

            return path;
        }

        /// <summary>
        /// Enumerates all nodes in this class
        /// </summary>
        private IEnumerable<Method> AllNodes
        {
            get
            {
                var node = FirstNode;
                while (node != null)
                {
                    yield return node?.Method;

                    if (node == this.LastNode)
                    {
                        yield break;
                    }

                    node = this.GetNextNode(node);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private MethodNode GetNextNode(MethodNode current)
        {
            var version = this.treePath.ContainsKey(current.NodeId) ? 
                this.treePath[current.NodeId] : 0;

            return current.GetNextNode(version);
        }

        /// <summary>
        /// Adds a node to the end of this code path
        /// </summary>
        /// <param name="node">node to add</param>
        private CodePath AddLast(MethodNode node)
        {
            return AddLast(node, node);
        }

        /// <summary>
        /// Adds a node to the end of this code path
        /// </summary>
        /// <param name="node">node to add</param>
        /// <param name="lastNode">the new last node</param>
        private CodePath AddLast(MethodNode node, MethodNode lastNode)
        {
            if (FirstNode == null)
            {
                FirstNode = node;
            }

            if (LastNode != null)
            {
                var version = LastNode.AddNextNode(node);
                this.treePath[LastNode.NodeId] = version;
            }

            LastNode = lastNode;
            this.Length += 1;
            return this;
        }
    }
}
