using CodePathFinder.CodeAnalysis;
using CodePathFinder.CodeAnalysis.Logging;
using CodePathFinder.CodeAnalysis.PathFinding;
using CodePathFinder.MonoCecilImpl;
using CodePathFinder.MonoCecilImpl.CodeAnalysis;
using CodePathFinder.MonoCecilImpl.Utility;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            this.start = start ?? throw new ArgumentNullException(nameof(start));
            this.end = end ?? throw new ArgumentNullException(nameof(end));
            this.options = options ?? new AssemblyMetadataOption[0];

            this.buttonStart.Click += ButtonStart_Click;
            this.buttonCancel.Click += ButtonCancel_Click;
            this.assemblyFolder = assemblyFolder;
            this.source = new CancellationTokenSource();

            this.textStartMethod.Text = this.start.FullName;
            this.textEndMethod.Text = this.end.FullName;

            this.textStartMethod.ReadOnly = true;
            this.textEndMethod.ReadOnly = true;

            this.numericDepthLImit.Value = 25;
            this.checkLimitPathDepth.Checked = true;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void ButtonStart_Click(object sender, EventArgs e)
        {
            this.tabViewSwitcher.SelectedIndex = 1;
            var assemblyLoader = new MonoCecilAssemblyLoader(assemblyFolder);
            var assemblies = assemblyLoader.LoadDomainAssemblies(options);
            var asmGraphAnalyzer = new MonoCecilAssemblyGraphAnalyzer(assemblies, new TypeDefinitionUtility());
            var pathFinder = new DepthFirstCodePathFinder(asmGraphAnalyzer);

            var limit = this.checkLimitPathDepth.Checked ? (int)this.numericDepthLImit.Value : -1;
            try
            {
                await pathFinder.ConstructPartialPaths(start, end, source.Token);

                this.labelSearchStatus.Text = "Constructing the result tree from the partial paths...";

                var pathEnumerable = pathFinder.ConstructFullPaths(start, end, source.Token, limit);
                var paths = await Task.Run(() => pathEnumerable.ToList(), source.Token);

                var viewer = new ResultsViewerLanding(paths, start, end);
                viewer.Show();
            }
            catch (OperationCanceledException ex)
            {
                AppLogger.Current.Error(ex, "Path finding operation was cancelled!");

                MessageBox.Show("The search was cancelled.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            Close();
        }

        private void CheckLimitPathDepth_CheckedChanged(object sender, EventArgs e)
        {
            this.numericDepthLImit.Enabled = this.checkLimitPathDepth.Checked;
        }

        private void ButtonAbort_Click(object sender, EventArgs e)
        {
            this.source.Cancel();
        }
    }

    /// <summary>
    /// Tab control without the tabs (for view-changing functionality)
    /// </summary>
    public class TablessControl : TabControl
    {
        protected override void WndProc(ref Message m)
        {
            // Hide tabs by trapping the TCM_ADJUSTRECT message
            if (m.Msg == 0x1328 && !DesignMode) m.Result = (IntPtr)1;
            else base.WndProc(ref m);
        }
    }
}
