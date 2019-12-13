using System;
using Autofac;
using TagsCloudApp.WordFiltering;
using TagsCloudApp.ToSizeConverter;
using TagsCloudApp.Reader;
using TagsCloudApp.Visualization;
using TagsCloudApp.LayOuter;
using TagsCloudApp.ImageSave;
using TagsCloudApp.App;

namespace TagsCloudApp
{
    public class Program
    {
        public static void Main(string[] args)
        {            
            var container = GetContainer();
            var tagsCloudCreator = container.Resolve<Application>();
            tagsCloudCreator.CreateImage(args);
            Console.ReadKey();
        }

        private static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<FileTextReader>().As<IFileReader>().SingleInstance();
            builder.RegisterType<Filter>().As<IWordFilter>().SingleInstance();
            builder.RegisterType<WordToSizeConverter>().As<IToSizeConverter>().SingleInstance();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().SingleInstance(); 
            builder.RegisterType<Visualisator>().As<IVisualisator>();
            builder.RegisterType<Saver>().As<IImageSaver>().SingleInstance();
            builder.RegisterType<Application>().AsSelf().SingleInstance();
            return builder.Build();
        }
    }
}
