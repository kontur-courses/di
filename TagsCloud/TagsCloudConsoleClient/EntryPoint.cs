using Autofac;
using System;
using System.Drawing;
using TagsCloudGenerator.Generators;
using TagsCloudGenerator.WordsParsers;
using TagsCloudGenerator.WordsConverters;
using TagsCloudGenerator.WordsFilters;
using TagsCloudGenerator.WordsLayouters;
using TagsCloudGenerator.RectanglesLayouters;
using TagsCloudGenerator.PaintersAndSavers;
using TagsCloudGenerator.Painters;
using TagsCloudGenerator.Savers;
using TagsCloudGenerator.PointsSearchers;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudConsoleClient
{
    internal class EntryPoint
    {
        private static void Main(string[] args)
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<DefaultTagsCloudGenerator>().As<ITagsCloudGenerator>();
            cb.RegisterType<DefaultWordsParser>().As<IWordsParser>().SingleInstance();
            cb.RegisterType<DefaultWordsLayouter>().As<IWordsLayouter>();
            cb.RegisterType<DefaultRectanglesLayouter>().As<IRectanglesLayouter>();
            cb.RegisterType<PointsSearcherOnSpiral>().As<IPointsSearcher>();
            cb.RegisterType<DefaultPainterAndSaver>().As<IPainterAndSaver>().SingleInstance();
            cb.RegisterType<PngSaver>().As<ISaver>().SingleInstance();

            cb.RegisterType<DefaultWordsConverter>().As<IWordsConverter>().SingleInstance();
            cb.RegisterType<DefaultWordsFilter>().As<IWordsFilter>().SingleInstance();

            cb.RegisterInstance(new DefaultPainter(new Color[] { Color.Red, Color.Blue, Color.Yellow }, Color.Black))
                .As<IPainter>()
                .SingleInstance();
            
            var container = cb.Build();
            var generator = container.Resolve<ITagsCloudGenerator>();
            //Console.WriteLine(generator.TryGenerate(/*"data.txt"*/, "Arial", new Size(1000, 1000), "image.png"));
        }
    }
}