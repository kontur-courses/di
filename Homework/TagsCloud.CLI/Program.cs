using System;
using System.Linq;
using Autofac;
using CommandLine;

namespace TagsCloud.Words
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args);
            if (result.Errors.Any())
                return;

            TagsCloudModuleSettings settings;
            try
            {
                settings = new SettingsCreator().Create(result.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            using var container = CreateContainer(settings).BeginLifetimeScope();

            container.Resolve<LayouterCore>().Run();
        }

        private static IContainer CreateContainer(TagsCloudModuleSettings settings)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new TagsCloudModule(settings));
            return builder.Build();
        }
    }
}