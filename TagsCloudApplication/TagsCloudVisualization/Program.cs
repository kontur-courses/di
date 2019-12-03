using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudLayout.CloudLayouters;
using Autofac;
using TextConfiguration;
using TextConfiguration.TextReaders;
using TagsCloudLayout.PointLayouters;
using TextConfiguration.WordFilters;
using TextConfiguration.WordProcessors;

namespace TagsCloudVisualization
{
    public static class Program
    {
        public static void Main()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Register(c => 
                new CloudTagProperties(FontFamily.GenericMonospace, 20))
                .As<CloudTagProperties>();
            containerBuilder.Register(c => 
                new VisualizatorProperties(new Size(800, 600)))
                .As<VisualizatorProperties>();

            containerBuilder.RegisterType<RawTextReader>()
                .As<ITextReader>();
            containerBuilder.RegisterType<BoringWordsFilter>()
                .As<IWordFilter>();
            containerBuilder.RegisterType<EmptyWordFilter>()
                .As<IWordFilter>();
            containerBuilder.RegisterType<ToLowerCaseProcessor>()
                .As<IWordProcessor>();
            containerBuilder.RegisterType<TextPreprocessor>()
                .As<TextPreprocessor>();
            containerBuilder.RegisterType<WordsProvider>()
                .As<WordsProvider>();
            containerBuilder.RegisterType<CloudTagProvider>()
                .As<CloudTagProvider>();

            containerBuilder.Register(c => new Point(400, 300))
                .As<Point>();
            containerBuilder.RegisterType<ArchimedeanSpiral>()
                .As<ICircularPointLayouter>();
            containerBuilder.RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>();
            containerBuilder.Register(c => 
                    new ConstantTextColorProvider(Color.FromArgb(127, 127, 0)))
                .As<ITextColorProvider>();

            containerBuilder.RegisterType<TagCloudVisualizator>()
                .As<TagCloudVisualizator>();

            var builder = containerBuilder.Build();

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Words.txt");
            var image = builder.Resolve<TagCloudVisualizator>()
                .VisualizeCloudTags(builder.Resolve<CloudTagProvider>().ReadCloudTags(filePath));
            image.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
                "Cloud.png"), ImageFormat.Png);
        }
    }
}
