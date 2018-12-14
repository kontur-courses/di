namespace TagsCloudVisualization
{
    public class ConsoleApplicationRunner : IApplicationRunner
    {
        private ConsoleApplication app;

        public ConsoleApplicationRunner(ConsoleApplication application)
        {
            app = application;
        }

        public void Run(string[] args)
        {
            app.GenerateImage(args);
        }
    }
}
