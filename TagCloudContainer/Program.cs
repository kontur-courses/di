using System;
using Autofac;
using TagsCloudPreprocessor;
using TagsCloudVisualization;

namespace TagCloudContainer
{
    class Program
    {
        private static IContainer container;

        private static void InitContainer(Config config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleClient>().As<IUserInterface>();

            builder.RegisterType<Config>().AsSelf().SingleInstance();

            builder.RegisterType<XmlWordExcluder>().As<IWordExcluder>();
            builder.RegisterType<TxtFileReader>().As<IFileReader>();
            builder.RegisterType<TextParser>().As<ITextParser>();
            builder.RegisterType<SimpleWordsValidator>().As<IWordsValidator>();
            builder.RegisterType<Preprocessor>().As<IPreprocessor>();

            builder.RegisterType<ArchimedesSpiral>()
                .WithParameter("center", config.Center)
                .As<ISpiral>();
            builder.RegisterType<CloudLayouter>().As<ICloudLayouter>();

            builder.RegisterType<TagCloudVisualization>()
                .WithParameters(
                    new[]
                    {
                        new NamedParameter("font", config.Font),
                        new NamedParameter("color", config.Color),
                        new NamedParameter("backgroundColor", config.BackgroundColor),
                    })
                .As<ITagCloudVisualization>();

            builder.RegisterType<TagCloudProgram>()
                .WithParameter("config", config)
                .AsSelf();

            container = builder.Build();
        }

        static void Main(string[] args)
        {
            var client = new ConsoleClient();
            var config = client.GetConfig(args, out var toExit);
            if (toExit)
                Environment.Exit(0);

            InitContainer(config);
            
            container.Resolve<TagCloudProgram>().SaveTagCloud();
        }
    }
}