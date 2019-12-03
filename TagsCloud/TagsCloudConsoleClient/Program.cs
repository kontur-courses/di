using Autofac;
using TagsCloud.FileReader;
using TagsCloud.Interfaces;
using TagsCloud.CloudLayouter;
using TagsCloud.WordProcessing;
using TagsCloud.FinalProcessing;
using CommandLine;
using System.Drawing;
using System.Reflection;
using System;

namespace TagsCloud
{
    class Program
    {

        private static IContainer Container { get; set; }
        class Options
        {
            [Value(0, Required = true, HelpText = "Input files with words.")]
            public string InputFiles { get; set; }

            [Value(1, Required = true, HelpText = "Result file.")]
            public string savePath { get; set; }

            [Option('w', "width", Default=1920, HelpText = "Width result image.")]
            public int width { get; set; }

            [Option('h', "height", Default = 1080, HelpText = "Height result image.")]
            public int height { get; set; }

            [Option('b', "background", Default ="White", HelpText = "Background color.")]
            public string backgroundColor { get; set; }

            [Option('f', "font", Default = "Comic Sans MS", HelpText = "Font name.")]
            public string fontName { get; set; }

            [Option('s', "splitter", Default = "WhiteSpace", HelpText = "Split by line or white space. (Line || WhiteSpace)")]
            public string splitType { get; set; }

            [Option('a', "angel", Default = 3.14, HelpText = "Delta radius between tusrn spiral.")]
            public double angel{ get; set; }

            // Omitting long name, defaults to name of property, ie "--verbose"
        }

        static void Main(string[] args)
        {
            var inputPath = "";
            var outputPath = "";
            var size = Size.Empty;
            var backgroundColor = Color.Empty;
            var fontName = "";
            var splitType = "";
            var deltaRadiusBetweenTurns = 1.0;
            Parser.Default.ParseArguments<Options>(args)
              .WithParsed<Options>(opts =>
              {
                  inputPath = opts.InputFiles;
                  outputPath = opts.savePath;
                  size = new Size(opts.width, opts.height);
                  backgroundColor = Color.FromName(opts.backgroundColor);
                  fontName = opts.fontName;
                  splitType = opts.splitType;
                  deltaRadiusBetweenTurns = opts.angel;
              });
            if (string.IsNullOrEmpty(inputPath) || string.IsNullOrEmpty(outputPath))
                return;

            var dataAccess = Assembly.GetExecutingAssembly();

            var container = new ContainerBuilder();
            container.RegisterAssemblyTypes(dataAccess).AsImplementedInterfaces();

            if (splitType == "Line")
                container.RegisterType<SpliterByLine>().As<ITextSpliter>();
            else if (splitType == "WhiteSpace")
                container.RegisterType<SpliterByWhiteSpace>().As<ITextSpliter>();
            else
            {
                throw new ArgumentException($"Unsupported split format {splitType}.");
            }

            if (deltaRadiusBetweenTurns < 1 || deltaRadiusBetweenTurns > 10)
                throw new ArgumentException("Delta radius between turns spiral must be more than 1 but less than 10.");

            container.RegisterType<RoundSpiralPositionGenerator>().As<IPositionGenerator>()
                .WithParameter(new TypedParameter(typeof(double), deltaRadiusBetweenTurns));

            container.RegisterType<DefaultFontGenerator>().As<IFontSettingsGenerator>()
                .WithParameter(new TypedParameter(typeof(string), fontName)).SingleInstance();

            container.RegisterType<TagCloudVisualizer>().AsSelf();

            Container = container.Build();

            Container.Resolve<TagCloudVisualizer>().GenerateTagCloud(inputPath, outputPath, size, backgroundColor);
        }
    }
}
