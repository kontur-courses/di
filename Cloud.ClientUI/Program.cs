using Microsoft.Extensions.DependencyInjection;

namespace Cloud.ClientUI
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var argumentParser = new ArgumentParser();
            var parsedArguments = argumentParser.Parse(args);
            var containerBuilder = new ContainerBuilder();
            var container = containerBuilder.CreateContainer(parsedArguments);
            container.BuildServiceProvider().GetService<TagCloudCreator>().Run();
        }
    }
}