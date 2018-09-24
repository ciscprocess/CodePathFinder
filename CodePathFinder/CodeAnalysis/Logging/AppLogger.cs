namespace CodePathFinder.CodeAnalysis.Logging
{
    public static class AppLogger
    {
        public static void RegisterLogger(ICodePathLogger instance)
        {
            Current = instance;
        }

        public static ICodePathLogger Current { get; private set; } = new NullLogger();
    }
}
