using CodePathFinder.CodeAnalysis;
using CodePathFinder.CodeAnalysis.PathFinding;
using CodePathFinder.MonoCecilImpl;
using CodePathFinder.MonoCecilImpl.CodeAnalysis;
using CodePathFinder.MonoCecilImpl.Utility;
using System;
using System.Threading;
using System.Windows.Forms;

namespace CodePathFinder.VisualUtility
{
    public partial class StartSearchPage : Form
    {
        private string assemblyFolder;
        private AssemblyMetadataOption[] options;
        private Method start;
        private Method end;
        private CancellationTokenSource source;

        public StartSearchPage(string assemblyFolder,
            AssemblyMetadataOption[] options,
            Method start,
            Method end)
        {
            InitializeComponent();
            this.Load += StartSearchPage_Load;
            this.assemblyFolder = assemblyFolder;
            this.options = options;
            this.start = start;
            this.end = end;
            this.source = new CancellationTokenSource();
        }

        private void StartSearchPage_Load(object sender, EventArgs e)
        {
            var assemblyLoader = new MonoCecilAssemblyLoader(assemblyFolder);
            var assemblies = assemblyLoader.LoadDomainAssemblies(options);
            var asmGraphAnalyzer = new MonoCecilAssemblyGraphAnalyzer(assemblies, new TypeDefinitionUtility());
            var pathFinder = new DepthFirstCodePathFinder(asmGraphAnalyzer);
            var source = new CancellationTokenSource();
            pathFinder.FindPathsBetweenMethods(start, end, source.Token, 20).ContinueWith(x =>
            {
                if (x.IsFaulted)
                {
                    MessageBox.Show("An error occured.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (x.IsCanceled)
                {
                    MessageBox.Show("operation cancelled");
                }

                if (this.InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        this.labelStatus.Text = "Finished!";
                        var viewer = new ResultsViewerLanding(x.Result, start, end);
                        viewer.Show();
                    }));
                }
            });
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            source.Cancel();
        }
    }
}
