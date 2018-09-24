using CodePathFinder.CodeAnalysis.Logging;
using CodePathFinder.VisualUtility.Logging;
using System;
using System.Windows.Forms;

namespace CodePathFinder.VisualUtility
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppLogger.RegisterLogger(
                new NLogLogger(
                    NLog.LogManager.GetCurrentClassLogger()
                ));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LandingPage());
        }
    }
}
