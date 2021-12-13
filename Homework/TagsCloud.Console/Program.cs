using Autofac;
using TagsCloudContainer;

namespace TagsCloud.Console
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var appSettings = AppSettings.Parse(args);
            var tagCloudSettings = TagCloudSettings.Parse(args);
            using var container = Configure(appSettings, tagCloudSettings);
            container.Resolve<IConsoleUI>()
                .Run(appSettings, tagCloudSettings);
        }

        internal static IContainer Configure(IAppSettings settings, ITagCloudSettings tagCloudSettings)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(settings).AsImplementedInterfaces().SingleInstance();
            builder.RegisterInstance(tagCloudSettings).AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ConsoleUI>().AsImplementedInterfaces();
            builder.RegisterModule<InfrastructureModule>();
            return builder.Build();
        }
    }
}