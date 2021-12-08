using System.Linq;
using Autofac;
using CommandLine;
using TagsCloud.Visualization;

namespace TagsCloud.Words
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args);
            if (result.Errors.Any())
                return;

            var settings = new SettingsCreator().Create(result.Value);

            using var container = CreateContainer(settings).BeginLifetimeScope();

            container.Resolve<CliLayouterCore>().Run();
        }

        private static IContainer CreateContainer(TagsCloudModuleSettings settings)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new TagsCloudModule(settings));
            builder.RegisterType<CliLayouterCore>().AsSelf();
            return builder.Build();
        }
    }
}