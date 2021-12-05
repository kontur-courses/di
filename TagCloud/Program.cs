using Autofac;
using TagCloud.UI;
using TagCloud.Readers;
using TagCloud.Analyzers;
using TagCloud.Creators;
using TagCloud.Layouters;
using TagCloud.UI.Console;
using TagCloud.Visualizers;
using TagCloud.Writers;


namespace TagCloud
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TextReader>().As<IFileReader>().SingleInstance();
            builder.RegisterType<TextAnalyzer>().As<ITextAnalyzer>().SingleInstance();
            builder.RegisterType<FrequencyAnalyzer>().As<IFrequencyAnalyzer>().SingleInstance();
            builder.RegisterType<TagCreatorFactory>().As<ITagCreatorFactory>().SingleInstance();
            builder.RegisterType<CircularCloudLayouterFactory>().As<ICloudLayouterFactory>().SingleInstance();
            builder.RegisterType<CloudVisualizer>().As<IVisualizer>().SingleInstance();
            builder.RegisterType<BitmapWriter>().As<IFileWriter>().SingleInstance();

            builder.RegisterType<ConsoleUI>().As<IUserInterface>().SingleInstance();

            var client = builder.Build().Resolve<IUserInterface>();
            client.Run(args);
        }
    }
}
