namespace CodePathFinder
{
    public class AssemblyLoadProgressUpdate
    {
        public int TotalAssemblies { get; set; }
        public int CurrentAssemblyNumber { get; set; }
        public string CurrentAssemblyName { get; set; }
        public string Operation { get; set; }
    }
}
