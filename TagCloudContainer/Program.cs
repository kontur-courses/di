using System;
using Autofac;
using Autofac.Core;
using TagsCloudPreprocessor;
using TagsCloudPreprocessor.Preprocessors;
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

            builder.RegisterType<XmlWordExcluder>().As<IWordExcluder>();
            builder.RegisterType<TxtFileReader>().As<IFileReader>();
            builder.RegisterType<TextParser>().As<ITextParser>();
            builder.RegisterType<WordsExcluder>().Named<IPreprocessor>("WordsExcluder");
            builder.RegisterType<WordsStemer>().Named<IPreprocessor>("WordsStemer");

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
                .WithParameters(
                    new Parameter[]
                    {
                        new NamedParameter("config", config),
                        new ResolvedParameter(
                            (pi, ctx) => pi.Name == "wordsExcluder",
                            (pi, ctx) => ctx.ResolveNamed<IPreprocessor>("WordsExcluder")),
                        new ResolvedParameter(
                            (pi, ctx) => pi.Name == "wordsStemer",
                            (pi, ctx) => ctx.ResolveNamed<IPreprocessor>("WordsStemer")),
                    })
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