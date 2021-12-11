using Autofac;
using TagCloud.UI;
using TagCloud.Readers;
using TagCloud.Analyzers;
using TagCloud.Creators;
using TagCloud.Layouters;
using TagCloud.UI.Console;
using TagCloud.Visualizers;
using TagCloud.Writers;


namespace TagCloud
{
    public class Program
    {
        private static void Main(string[] args)
        {
            using var container = DependencyConfigurator.GetConfiguredContainer();
            var client = container.Resolve<IUserInterface>();
            client.Run(args);
        }
    }
}
