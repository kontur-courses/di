using Autofac;
using CommandLine;
using TagCloudDi.Applications;

namespace TagCloudDi
{
    abstract class MainClass
    {
        public static void Main(string[] args)
        {
            var settings = Parser.Default.ParseArguments<Settings>(args).Value;
            var container = Container.SetupContainer(settings);
            var app = container.Resolve<IApplication>();
            app.Run();
        }
    }
}
