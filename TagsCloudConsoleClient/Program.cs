using Autofac;
using TagsCloud.FileReader;
using TagsCloud.Interfaces;
using TagsCloud.CloudLayouter;
using TagsCloud.WordProcessing;
using TagsCloud.FinalProcessing;
using CommandLine;
using System.Drawing;

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

            [Option('w', "width", Default = 1920, HelpText = "Width result image.")]
            public int width { get; set; }

            [Option('h', "height", Default = 1080, HelpText = "Height result image.")]
            public int height { get; set; }

            [Option('b', "background", Default = "White", HelpText = "Background color.")]
            public string backgroundColor { get; set; }

            [Option('f', "font", Default = "Comic Sans MS", HelpText = "Font name.")]
            public string fontName { get; set; }

            [Option('s', "splitter", Default = 1, HelpText = "Split by line. 0 - split by line. 1 - split by white space")]
            public int splitType { get; set; }

            // Omitting long name, defaults to name of property, ie "--verbose"
        }

        static void Main(string[] args)
        {
            var inputPath = "";
            var outputPath = "";
            var size = Size.Empty;
            var backgroundColor = Color.Empty;
            var fontName = "";
            var splitType = 0;
            Parser.Default.ParseArguments<Options>(args)
              .WithParsed<Options>(opts =>
              {
                  inputPath = opts.InputFiles;
                  outputPath = opts.savePath;
                  size = new Size(opts.width, opts.height);
                  backgroundColor = Color.FromName(opts.backgroundColor);
                  fontName = opts.fontName;
                  splitType = opts.splitType;
              });
            if (string.IsNullOrEmpty(inputPath) || string.IsNullOrEmpty(outputPath))
                return;

            var container = new ContainerBuilder();
            container.RegisterType<TxtReader>().As<IFileReader>();
            container.RegisterType<DefaultPathValidator>().As<IPathValidator>();
            container.RegisterType<CircularCloudLayouter>().As<ITagCloudLayouter>().SingleInstance();
            container.RegisterType<RoundSpiralPositionGenerator>().As<IPositionGenerator>().SingleInstance();
            if (splitType == 1)
                container.RegisterType<SpliterByWhiteSpace>().As<ITextSpliter>();
            else
                container.RegisterType<SpliterByLine>().As<ITextSpliter>();
            container.RegisterType<DefaultWordHandler>().As<IWordHandler>();
            container.RegisterType<WordStream>().As<IWordStream>();
            container.RegisterType<DefaultWordValidator>().As<IWordValidator>();
            container.RegisterType<DictionaryCounter>().As<IWordCounter>().SingleInstance();
            container.RegisterType<TagGenerator>().As<ITagGenerator>().SingleInstance();
            container.RegisterType<DefaultFontGenerator>().As<IFontSettingsGenerator>()
                .WithParameter(new TypedParameter(typeof(string), fontName)).SingleInstance();
            container.RegisterType<DefaultColorScheme>().As<IColorScheme>().SingleInstance();
            container.RegisterType<PngImageSaver>().As<IImageSaver>();
            container.RegisterType<TagCloudGenerator>().As<ITagCloudGenerator>().SingleInstance();
            container.RegisterType<DefaultCloudDrawer>().As<ICloudDrawer>().SingleInstance();
            container.RegisterType<TagCloudVisualizer>().AsSelf();

            Container = container.Build();

            Container.Resolve<TagCloudVisualizer>().GenerateTagCloud(inputPath, outputPath, size, backgroundColor);
        }
    }
}
