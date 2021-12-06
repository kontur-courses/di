using System.Drawing;
using Autofac;
using DeepMorphy;
using TagsCloudVisualization;
using TagsCloudVisualization.Interfaces;

namespace ConsoleClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<FileReader>().As<IFileReader>();
            builder.RegisterType<WordPreparator>().As<IWordPreparator>();
            builder.RegisterType<ImageGenerator>().As<IImageGenerator>();
            builder.RegisterType<FrequencyCounter>().As<IFrequencyCounter>();
            builder.RegisterType<TagCloudCreator>().As<ITagCloudCreator>();

            var cloudLayouter = new CircularCloudLayouter(new Point(800, 800));
            builder.RegisterInstance(cloudLayouter).As<ICloudLayouter>();

            builder.RegisterInstance(new MorphAnalyzer());
            builder.RegisterInstance(new Bitmap(1600, 1600));
            builder.RegisterInstance(new Pen(Color.Red));
            builder.RegisterInstance(new Font(FontFamily.GenericMonospace, 12));

            var container = builder.Build();

            var cloudCreator = container.Resolve<ITagCloudCreator>();
            cloudCreator.CreateAndSaveCloudFromTo("Sample.txt", "Sample.png");
        }
    }
}