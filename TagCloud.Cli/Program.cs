using TagCloudCreator;

namespace TagCloud
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var configuration = Configuration.FromArguments(args);
            var container = ContainerBuilder.ConfigureContainer(configuration);
            var app = container.Resolve<Application>();
            app.Run(configuration.InputFile, configuration.OutputFile);
        }
    }
}