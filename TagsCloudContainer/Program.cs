using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommandLine;
using Autofac;
using TagsCloudContainer.Reader;
using TagsCloudContainer.WordProcessor;
using TagsCloudContainer.WordsToSizesConverter;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.RectanglesShifter;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.ImageSaver;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = GetContainer();

            var reader = container.Resolve<IFileReader>();
            //Позже сделаю получение имени файла из ввода
            var filename = @"Texts\words.txt";
            var words = reader.ReadWords(filename);

            var wordProcessor = container.Resolve<IWordProcessor>();
            var wordFrequencies = wordProcessor.ProcessWords(words);

            var converter = container.Resolve<IWordFrequenciesToSizesConverter>();
            var sizes = converter.ConvertToSizes(wordFrequencies);

            var layouter = container.Resolve<ILayouter>();
            var rectangles = layouter.GetRectangles(sizes);

            var shifter = container.Resolve<IRectanglesShifter>();
            var center = container.Resolve<Point>();
            rectangles = shifter.ShiftRectangles(rectangles, center);

            words = wordFrequencies.Select(p => p.Key).ToList();
            var wordRectangles = words.Zip(rectangles, (w, r) => new WordRectangle(w, r)).ToList();
            var visualizer = container.Resolve<IVisualizer>();
            var bitmap = visualizer.GetImage(wordRectangles);

            var saver = container.Resolve<IImageSaver>();
            //Позже сделаю получение имени файла из ввода
            saver.SaveImage(bitmap, "Images", "image.png");
        }

        private static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TextReader>().As<IFileReader>();
            builder.RegisterType<BasicWordProcessor>().As<IWordProcessor>();
            builder.RegisterType<WordFrequenciesToSizesConverter>().As<IWordFrequenciesToSizesConverter>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<CenterRectanglesShifter>().As<IRectanglesShifter>();
            builder.RegisterType<DefaultVisualizerSettings>().As<IVisualizerSettings>();
            builder.RegisterType<TagCloudVisualizer>().As<IVisualizer>();
            builder.RegisterType<Saver>().As<IImageSaver>();
            builder.Register(c => new Point(0, 0)).AsSelf();
            /*
            Позже сделаю получение размера изображения из ввода,
            Либо возможность указать, чтобы рассчиталось автоматически
            */
            builder.Register(c => new Size(10000, 10000)).AsSelf();
            return builder.Build();
        }
    }
}
