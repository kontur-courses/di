using System;
using Ninject;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Autofac;
using Ninject.Parameters;
using TagCloud;
using TagCloud.Implementations;
using TagCloud.Interfaces;
using CommandLine;
using CommandLine.Text;

namespace TagCloudMakerCUI
{
    class Option
    {
        [Option('i', "inputFile", Required = true, HelpText = "Input file path.")]
        public string InputFilePath { get; set; }

        [Option('e', "excludFile", Required = false, HelpText = "File with words to exclude.")]
        public string ExcludingFilePath { get; set; }

        [Option('f', "fintSize", Required = true, HelpText = "Font size in pixels.")]
        public int FontSize { get; set; }

        [Option('b', "background", Required = true, HelpText = "Background color.")]
        public string BackColor { get; set; }

        [Option('c', "textColor", Required = true, HelpText = "Text color.")]
        public string TextColor { get; set; }

        [Option('w', "width", Required = true, HelpText = "Image width.")]
        public int Width { get; set; }

        [Option('h', "height", Required = true, HelpText = "Image height.")]
        public int Height { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var option = new Option();
            var isValid = Parser.Default.ParseArguments(args, option);
            
            if (!isValid)
            {
                Console.WriteLine("Not all required arguments was passed.");
                return;
            }

            var excludingWords = string.IsNullOrWhiteSpace(option.ExcludingFilePath)
                ? new string[0]
                : File.ReadLines(option.ExcludingFilePath);
            using (var scope = GetContainer(excludingWords).BeginLifetimeScope())
            {
                var maker = scope.Resolve<ITagCloudMaker>();
                var path = maker.CreateTagCloud(option.InputFilePath, option.FontSize,
                    new DrawingSettings(Color.FromName(option.BackColor), Color.FromName(option.TextColor), 
                    FontFamily.GenericMonospace, new Size(option.Width, option.Height), ImageFormat.Png));
                Console.WriteLine(path);
            }
        }

        static IContainer GetContainer(IEnumerable<string> badWords)
        {
            var container = new ContainerBuilder();
            container.RegisterType<MystemShell>().As<IMystemShell>();
            container.RegisterType<WordProcessor>().As<IWordProcessor>().WithParameter("badWords", badWords);
            container.RegisterType<SpiralPointComputer>().As<IPointComputer>().WithParameter("center", new Point(0, 0));
            container.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            container.RegisterType<TagCloudDrawer>().As<ITagCloudDrawer>();
            container.RegisterType<ImageSaver>().As<IImageSaver>();
            container.RegisterType<TagCloudMaker>().As<ITagCloudMaker>();
            return container.Build();
        }
    }
}
