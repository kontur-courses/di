using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CommandLine;
using Autofac;
using TagsCloudContainer.Reader;
using TagsCloudContainer.WordProcessor;
using TagsCloudContainer.WordsToSizesConverter;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.RectanglesTransformer;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.ImageSaver;

namespace TagsCloudContainer
{
    class Program
    {
        public class Options
        {
            [Option('i', "input", Required = true, HelpText = "Path of input file with words")]
            public string Input { get; set; }

            [Option('o', "output", Required = true, HelpText = "Path of output image file")]
            public string Output { get; set; }

            [Option('w', "width", Required = false, HelpText = "Image width")]
            public int Width { get; set; }

            [Option('h', "height", Required = false, HelpText = "Image height")]
            public int Height { get; set; }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(o =>
            {
                if (!File.Exists(o.Input))
                {
                    Console.WriteLine("Input file not exists");
                    return;
                }

                var inputPath = new FileInfo(o.Input).FullName;
                var outputPath = new FileInfo(o.Output).FullName;
                var imageSize = new Size(5000, 5000);
                if (o.Width > 0 && o.Height > 0)
                    imageSize = new Size(o.Width, o.Height);
                var container = GetContainer(inputPath, imageSize);
                var tagsCloudCreator = container.Resolve<TagsCloudCreator>();
                tagsCloudCreator.CreateImage(outputPath);
            });
        }

        private static IContainer GetContainer(string inputPath, Size imageSize)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new FileReader(inputPath)).As<ITextReader>().SingleInstance();
            builder.RegisterType<BasicWordProcessor>().As<IWordProcessor>().SingleInstance();
            builder.RegisterType<WordFrequenciesToSizesConverter>().As<IWordFrequenciesToSizesConverter>().SingleInstance();
            builder.RegisterType<DefaultLayouterSettings>().As<ILayouterSettings>().SingleInstance();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>().SingleInstance();
            builder.RegisterInstance(
                    new ShifterSettings(
                        new DefaultLayouterSettings(), imageSize))
                .As<IShifterSettings>().SingleInstance();
            builder.RegisterType<CenterRectanglesShifter>().As<IRectanglesTransformer>().SingleInstance();
            builder.RegisterInstance(new DefaultVisualizerSettings(imageSize)).As<IVisualizerSettings>().SingleInstance();
            builder.RegisterType<TagCloudVisualizer>().As<IVisualizer>().SingleInstance();
            builder.RegisterType<Saver>().As<IImageSaver>().SingleInstance();
            builder.RegisterType<TagsCloudCreator>().AsSelf().SingleInstance();
            return builder.Build();
        }
    }
}
