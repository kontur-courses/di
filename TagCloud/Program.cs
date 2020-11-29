using System;
using System.Drawing;
using Autofac;
using TagCloud.Drawers;
using TagCloud.DataReaders;
using TagCloud.Layouters;
using TagCloud.TextAnalyzer;
using TagCloud.TextAnalyzer.WordNormalizer;

namespace TagCloud
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: DI!!!!!!!
            var containerBuilder = new ContainerBuilder();
            // Временно константы зарегистрировал
            containerBuilder.Register(_ => new Point(1000, 1000));
            containerBuilder.Register(_ => new Size(2000, 2000));
            containerBuilder.RegisterType<WordNormalizer>().As<IWordNormalizer>();
            containerBuilder.RegisterType<StandardAnalyzer>().As<ITextAnalyzer>();
            containerBuilder.RegisterType<CircularCloudLayouter>().As<IRectangleLayouter>();
            containerBuilder.RegisterType<TagCloudDrawer>().As<ITagCloudDrawer>();
            containerBuilder.RegisterType<TextFileReader>().As<IDataReader>();
            containerBuilder.RegisterType<Picture>().As<IPicture>();
            containerBuilder.RegisterType<TagCloud>().AsSelf();

            var container = containerBuilder.Build();
            
            var tagCloud = container.Resolve<TagCloud>();
            tagCloud.MakeTagCloud("c:/shpora/food.txt");
        }
    }
}