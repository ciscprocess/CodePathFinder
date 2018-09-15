using CodePathFinder.CodeAnalysis;
using CodePathFinder.CodeAnalysis.PathFinding;
using CodePathFinder.MonoCecilImpl;
using CodePathFinder.MonoCecilImpl.CodeAnalysis;
using CodePathFinder.MonoCecilImpl.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodePathFinder.VisualUtility
{
    public partial class LandingPage : Form
    {
        /// <summary>
        /// Default path to domain assemblies
        /// </summary>
        private const string DefaultAsmPath =
            @"C:\Windows\Microsoft.NET\assembly\GAC_64\System.Web\v4.0_4.0.0.0__b03f5f7f11d50a3a";

        /// <summary>
        /// Default "include" options when loading assemblies
        /// </summary>
        private AssemblyMetadataOption[] options = new AssemblyMetadataOption[0];

        private Method start = null;
        private Method end = null;

        public LandingPage()
        {
            InitializeComponent();
            this.assemblyTreeViewer1.Handler = method => 
            {
                this.textBox1.Text = method.FullName;
                this.start = method;
            };

            this.assemblyTreeViewer2.Handler = method =>
            {
                this.textBox2.Text = method.FullName;
                this.end = method;
            };

            this.progressBar1.Maximum = 0;
            this.progressBar1.Minimum = 0;

            this.textAsmLocation.Text = DefaultAsmPath;
            this.buttonFindPaths.Click += ButtonFindPaths_Click;
        }

        private void ButtonFindPaths_Click(object sender, EventArgs e)
        {
            var location = this.textAsmLocation.Text;
            var assemblyLoader = new MonoCecilAssemblyLoader(location);
            var assemblies = assemblyLoader.LoadDomainAssemblies(options);
            var asmGraphAnalyzer = new MonoCecilAssemblyGraphAnalyzer(assemblies, new TypeDefinitionUtility());
            var pathFinder = new DepthFirstCodePathFinder(asmGraphAnalyzer, 1);
            var allPaths = pathFinder.FindPathsBetweenMethods(start, end);

            var viewer = new ResultsViewerLanding(allPaths, start, end);
            viewer.Show();
        }

        private void buttonLoadAssemblies_Click(object sender, EventArgs e)
        {
            var location = this.textAsmLocation.Text;
            if (!Directory.Exists(location))
            {
                MessageBox.Show("The specified assembly directory does not exist!", 
                    "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);

                return;
            }

            this.assemblyTreeViewer1.ReloadAssemblies(location, options);
            this.assemblyTreeViewer2.ReloadAssemblies(location, options);
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            var result = this.folderAsmBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.textAsmLocation.Text = this.folderAsmBrowserDialog.SelectedPath;
            }
        }

        private void buttonSetFilters_Click(object sender, EventArgs e)
        {
            var form = new SetAssemblyLoadOptions(options);
            form.ShowDialog();

            this.options = form.Includes.Concat(form.Excludes).ToArray();
        }
    }
}
