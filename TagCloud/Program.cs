using Autofac;
using TagCloud.AppConfig;
using TagCloud.App;

namespace TagCloud
{
    public class Program
    {
        static void Main(string[] args)
        {
            var appConfig = new ConsoleAppConfigProvider(args).GetAppConfig();
            var container = ContainerConfig.Configure(appConfig);
            var app = container.Resolve<IApp>();
            app.Run(appConfig);
        }
    }
}

