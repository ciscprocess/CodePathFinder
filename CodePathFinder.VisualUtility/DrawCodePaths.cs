using CodePathFinder.CodeAnalysis;
using CodePathFinder.CodeAnalysis.PathFinding;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CodePathFinder.VisualUtility
{
    public class DrawCodePaths
    {
        public void Draw(Image destination, ISet<CodePath> pathsToDraw)
        {
            // calculate ordering
            var methodOrderMap = new Dictionary<Method, int>();

            foreach (var path in pathsToDraw)
            {
                var index = 0;
                foreach (var node in path)
                {
                    if (!methodOrderMap.ContainsKey(node))
                    {
                        methodOrderMap[node] = 0;
                    }

                    methodOrderMap[node] = methodOrderMap[node] > index ? methodOrderMap[node] : index;
                }
            }
        }
    }
}
