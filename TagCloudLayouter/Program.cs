using System;
using System.Drawing;
using System.Linq;
using Autofac;
using CommandLine;
using TagsCloudContainer;
using TagsCloudVisualization;

namespace TagCloudLayouter
{
    class Program
    {
        public class Options
        {
            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }

            [Option('c', "count", Default = 20, HelpText = "Input count of tags in cloud.")]
            public int Count { get; set; }

            [Option("font-name", Default = "Times New Roman", HelpText = "Input font name.")]
            public string FontName { get; set; }

            [Option("font-size", Default = 40.0f, HelpText = "Input font size.")]
            public float FontSize { get; set; }

            [Option('n', "name", Default = "Cloud", HelpText = "Input file name.")]
            public string FileName { get; set; }

            [Option("out-path", HelpText = "Input path to directory to save image.")]
            public string OutPath { get; set; }

            [Value(0, HelpText = "Path to directory to save.")]
            public string PathToSave { get; set; }
        }

        private static IContainer container;
        private static IContainer CreateContainer(Point center)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SimplePreprocessor>().As<IPreprocessor>();
            builder.RegisterType<TxtReader>().As<IReader>();
            builder.RegisterType<TextParser>().As<ITextParser>();

            builder.Register(ctx => new ArchimedesSpiral(center)).As<ISpiral>();
            builder.RegisterType<CloudLayouter>().As<ICloudLayouter>();

            return builder.Build();

        }
        static void Main(string[] args)
        {
            var center = new Point(500, 500);
            var inputFile = "";
            var count = 20;
            var fontName = "Times New Roman";
            var fontSize = 40.0f;
            var fileName = "SimpleCloud";
            var outPath = "";

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    count = o.Count;
                    fontName = o.FontName;
                    fontSize = o.FontSize;
                    inputFile = o.PathToSave;
                    fileName = o.FileName;
                    outPath = o.OutPath ?? Environment.CurrentDirectory;
                });

            container = CreateContainer(center);

            var font = new Font(fontName, fontSize);


            var layouter = container.Resolve<ICloudLayouter>();
            var proc = container.Resolve<IPreprocessor>();
            var text = container.Resolve<IReader>().ReadFromFile(inputFile);
            var allWords = container.Resolve<ITextParser>().GetWords(text);
            var validWords = proc
                .GetValidWords(allWords)
                .Take(count)
                .ToList();


            var vis = new TagCloudVisualization(layouter);
            vis.SaveTagCloud(fileName, outPath, font, validWords);
        }
    }
}
