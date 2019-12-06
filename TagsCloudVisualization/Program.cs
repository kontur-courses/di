using System.Drawing;
using System.Drawing.Imaging;
using Autofac;
using TagsCloudVisualization.CloudPainters;
using TagsCloudVisualization.TextFilters;
using TagsCloudVisualization.TextPreprocessing;
using TagsCloudVisualization.TextReaders;
using TagsCloudVisualization.Visualization;
using TagsCloudVisualization.WordConverters;

namespace TagsCloudVisualization
{
    public static class Program
    {
        public static void Main()
        {
            var imageOptions = new VisualisingOptions(new Font("Arial", 12, FontStyle.Bold), 
                new Size(600, 600), Color.Black, Color.Pink);
            var textName = "2.txt";
            var imageName = "01";
            
            var cloudCreator = GetContainer().Resolve<CloudCreator>();
            var cloud = cloudCreator.GetCloud(imageOptions, textName);
            ImageSaver.SaveImageToDefaultDirectory(imageName, cloud, ImageFormat.Png);
        }

        private static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MultiColorCloudPainter>().As<CloudPainter>();
            builder.RegisterType<LowerCaseWordConverter>().As<IWordConverter>();
            builder.RegisterType<ShortWordsFilter>().As<ITextFilter>();    
            builder.RegisterType<BoringWordsFilter>().As<ITextFilter>();
            builder.RegisterType<TxtReader>().As<ITextReader>();
            builder.RegisterType<WordsProvider>().AsSelf();
            builder.RegisterType<WordPreprocessor>().AsSelf();
            builder.RegisterType<CloudCreator>().AsSelf();
            return builder.Build();
        }
    }
}