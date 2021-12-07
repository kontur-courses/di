using System.Drawing;
using Autofac;
using TagCloud.TextHandlers;
using TagCloud.TextHandlers.Converters;
using TagCloud.TextHandlers.Filters;
using TagCloud.TextHandlers.Parser;
using TagsCloudVisualization;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.PointGenerator;
using IContainer = Autofac.IContainer;

namespace TagCloud
{
    public class Program
    {
        private static IContainer container;

        public static void Main(string[] args)
        {
            CompositionRootInitialize();
            var app = container.Resolve<IApp>();
            app.Run(args);
        }
        

        private static void CompositionRootInitialize()
        {
            var builder = new ContainerBuilder();
            RegisterTextHandlers(builder);
            builder.RegisterType<ConsoleApp.ConsoleApp>().As<IApp>();
            builder.RegisterType<Cache>().As<ICache>();
            builder.Register(c => new Spiral(0.1f, 0.5, new PointF(0, 0), c.Resolve<ICache>())).As<IPointGenerator>();
            builder.Register(c => new Visualizer(c.Resolve<ICloudLayouter>()));
            builder.RegisterType<CloudLayouter>().AsSelf().As<ICloudLayouter>();
            container = builder.Build();
        }

        private static void RegisterTextHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<TextReaderFacade>().As<IReader>();
            builder.RegisterType<WordsParser>().As<ITextParser>();
            builder.RegisterType<BoringWordsFilter>().As<IFilter>();
            builder.RegisterType<TextFilter>().As<ITextFilter>();
            builder.RegisterType<WordConverter>().As<IWordConverter>();
            builder.RegisterType<WordsCountStatistics>().AsSelf();
        }
    }
}