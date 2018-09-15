namespace CodePathFinder.CodeAnalysis.PathFinding
{
    using CodeAnalysis;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contains a <see cref="Method" /> datum
    /// </summary>
    public class MethodNode : ICloneable
    {
        /// <summary>
        /// Internal ID tracker
        /// </summary>
        private static ulong IdCounter = 0;

        /// <summary>
        /// Mutex stand-in for IdCounter
        /// </summary>
        private static object IdCounterMutex = new object();

        /// <summary>
        /// Next nodes; mapped by version
        /// </summary>
        private List<MethodNode> nextNodes;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodNode" /> class
        /// </summary>
        /// <param name="method">method to represent</param>
        public MethodNode(Method method)
        {
            this.Method = method;
            this.Identifier = this.Method.FullName;
            this.nextNodes = new List<MethodNode>();

            lock (IdCounterMutex)
            {
                this.NodeId = IdCounter++;
            }
        }

        public ulong NodeId { get; private set; }

        /// <summary>
        /// Identifier for this code path
        /// </summary>
        public object Identifier { get; private set; }

        /// <summary>
        /// The method reference for this node
        /// </summary>
        public Method Method { get; private set; }

        /// <summary>
        /// Gets the next node by version. If the version doesn't exist, returns
        /// latest node
        /// </summary>
        /// <param name="version">version of next node to retrieve</param>
        /// <returns>next node by version</returns>
        public MethodNode GetNextNode(int version)
        {
            if (this.nextNodes.Count == 0)
            {
                return null;
            }

            return this.nextNodes[version];
        }

        /// <summary>
        /// Sets the next node by version
        /// </summary>
        /// <param name="nextNode">the node to set</param>
        /// <returns>the index (for pathing) of the added node</returns>
        public int AddNextNode(MethodNode nextNode)
        {
            this.nextNodes.Add(nextNode);

            return this.nextNodes.Count - 1;
        }

        /// <summary>
        /// Clones this <see cref="MethodNode" />
        /// </summary>
        /// <returns>the function node</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Checks for equality with another <see cref="MethodNode"/>
        /// </summary>
        /// <param name="other">the other method node</param>
        /// <returns>true if equal</returns>
        public override bool Equals(object other)
        {
            var node = (MethodNode)other;
            return this.Method.Equals(node.Method);
        }

        /// <summary>
        /// Gets the hash code for the identifier
        /// </summary>
        /// <returns>the calculated hash code</returns>
        public override int GetHashCode()
        {
            return Identifier.GetHashCode();
        }

        public override string ToString()
        {
            return Method.ToString();
        }
    }
}
