using CodePathFinder.CodeAnalysis.PathFinding;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using CodePathFinder.CodeAnalysis;

namespace CodePathFinder.VisualUtility
{
    public partial class ResultViewer : Form
    {
        private readonly IList<CodePath> results;

        public ResultViewer(IList<CodePath> results, Method start, Method end)
        {
            this.results = results;
            InitializeComponent();

            var viewer = new GViewer();
            var graph = new Graph();

            foreach (var path in this.results)
            {
                Method previous = null;
                foreach (var node in path)
                {
                    if (previous != null)
                    {
                        var prevName = previous.ToString();
                        var nextName = node.ToString();
                        if (!graph.Edges.Any(x => x.Source == prevName && x.Target == nextName))
                        {
                            graph.AddEdge(prevName, nextName);
                        }
                    }

                    previous = node;
                }
            }

            var startNode = graph.FindNode(start.ToString());
            startNode.Attr.FillColor = Color.Red;
            var endNode = graph.FindNode(end.ToString());
            endNode.Attr.FillColor = Color.Green;
            viewer.Graph = graph;
            //associate the viewer with the form 
            this.SuspendLayout();
            viewer.Dock = DockStyle.Fill;
            this.Controls.Add(viewer);
            this.ResumeLayout();
        }
    }
}
