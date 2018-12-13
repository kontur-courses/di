using System;
using System.IO;
using System.Linq;
using Autofac;
using TagsCloudContainer.Configuration;
using TagsCloudContainer.Controller;
using TagsCloudContainer.DataReader;
using TagsCloudContainerCLI.CommandLineParser;

namespace TagsCloudContainerCLI
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new SimpleCommandLineParser().Parse(args);

            if (string.IsNullOrEmpty(configuration.PathToWordsFile)
                || string.IsNullOrEmpty(configuration.DirectoryToSave)
                || string.IsNullOrEmpty(configuration.OutFileName))
                return;

            var container = BuildContainer(configuration);

            var tagsCloudController = container.Resolve<ITagsCloudController>();

            tagsCloudController.Save();

            Console.WriteLine("Visualization has been saved to " +
                              Path.Combine(configuration.DirectoryToSave, configuration.OutFileName));

            Console.ReadKey();
        }

        private static IContainer BuildContainer(IConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(configuration)
                .As<IConfiguration>();

            builder.RegisterInstance(new FileReader())
                .As<IDataReader>();

            var dataAccess = AppDomain.CurrentDomain.GetAssemblies()
                .First(assembly => assembly.FullName.Contains("TagsCloudContainer,"));

            builder.RegisterAssemblyTypes(dataAccess)
                .AsImplementedInterfaces()
                .Except<SimpleCommandLineParser>()
                .Except<SimpleConfiguration>()
                .Except<IDataReader>();

            return builder.Build();
        }
    }
}