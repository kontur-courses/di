using Autofac;
using TagCloud.UI;


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
