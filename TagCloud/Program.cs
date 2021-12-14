using System.Drawing;
using Autofac;
using TagCloud.Apps;
using TagCloud.CloudLayouter;
using TagCloud.Configurations;
using TagCloud.PointGenerator;
using TagCloud.Templates;
using TagCloud.Templates.Colors;
using TagCloud.TextHandlers;
using TagCloud.TextHandlers.Converters;
using TagCloud.TextHandlers.Filters;
using TagCloud.TextHandlers.Parser;
using IContainer = Autofac.IContainer;

namespace TagCloud
{
    public static class Program
    {
        private static IContainer container;

        public static void Main(string[] args)
        {
            var configuration = new CommandLineConfigurationProvider(args).GetConfiguration();


            if (configuration == null)
                return;
            CompositionRootInitialize(configuration);
            var app = container.Resolve<IApp>();
            app.Run(configuration);
        }


        private static void CompositionRootInitialize(Configuration configuration)
        {
            var builder = new ContainerBuilder();
            RegisterTextHandlers(builder);
            RegisterCloudLayouter(builder, configuration);
            RegisterTemplateHandlers(builder, configuration);
            builder.RegisterType<ConsoleApp>().As<IApp>();
            builder.RegisterType<Visualizer>().As<IVisualizer>();
            container = builder.Build();
        }

        private static void RegisterTextHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<WordsReader>().As<IReader>();
            builder.RegisterType<WordsFromTextParser>().As<ITextParser>();
            builder.RegisterType<BoringWordsFilter>().As<IFilter>();
            builder.RegisterType<TextFilter>().As<ITextFilter>();
            builder.Register(_ => new WordConverter().Using(s => s.ToLower())).As<IWordConverter>();
            builder.RegisterType<FontSizeByCountCalculator>().AsSelf();
        }

        private static void RegisterTemplateHandlers(ContainerBuilder builder, Configuration configuration)
        {
            builder.Register(_ => configuration.FontFamily).As<FontFamily>();
            builder.RegisterType<Template>().As<ITemplate>();
            builder.Register((c, _) =>
                    new TemplateCreator(configuration.FontFamily, configuration.BackgroundColor,
                        configuration.ImageSize,
                        c.Resolve<IFontSizeCalculator>(), c.Resolve<IColorGenerator>(), c.Resolve<ICloudLayouter>()))
                .As<ITemplateCreator>();
            builder.RegisterType<WordParameter>().AsSelf();
            builder.Register(_ => configuration.ColorGenerator).As<IColorGenerator>();
            builder.Register(_
                    => new FontSizeByCountCalculator(Configuration.MinFontSize, Configuration.MaxFontSize))
                .As<IFontSizeCalculator>();
        }

        private static void RegisterCloudLayouter(ContainerBuilder builder, Configuration configuration)
        {
            builder.RegisterType<Cache>().As<ICache>();
            builder.Register(_ => configuration.PointGenerator).As<IPointGenerator>();
            builder.RegisterType<CloudLayouter.CloudLayouter>().AsSelf().As<ICloudLayouter>();
        }
    }
}