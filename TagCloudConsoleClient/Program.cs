using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Threading;
using Autofac;
using CommandLine;
using TagsCloudVisualization;
using TagsCloudVisualization.Default;

namespace TagCloudConsoleClient
{
    class Options
    {
        [Value(0, MetaName = "source", HelpText = "Path to source text file.", Required = true)]
        public string SourcePath { get; set; }
        
        [Value(1, MetaName = "result", HelpText = "Path to result image.", Required = true)]
        public string ResultPath { get; set; }
        
        [Value(2, MetaName = "width", HelpText = "Width of result image.", Required = true)]
        public int Width { get; set; }
        
        [Value(3, MetaName = "height", HelpText = "Height of result image.", Required = true)]
        public int Height { get; set; }

        [Option('f', "font", HelpText = "Set font. If unknown sets to default.")]
        public string Font { get; set; } = SystemFonts.DefaultFont.Name;

        [Option('m', "max", HelpText = "Set maximum words to render.")]
        public int MaxCount { get; set; } = 100;
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(Start);
        }

        static void Start(Options options)
        {
            var ext = Path.GetExtension(options.ResultPath)?.TrimStart('.');
            var format = TryGetImageFormat(ext);
            if (options.Height < 1 || options.Width < 1)
                WrongResolution();
            if (ext == null || format == null)
                WrongFormat();
            var font = new Font(options.Font, 10);
            var source = new FileInfo(options.SourcePath);
            if (!source.Exists)
                SourceNotExist();
            var tagCloud = ConfigureTagCloud();
            var resolution = new Size(options.Width, options.Height);
            try
            {
                tagCloud.CreateTagCloudFromFile(source, font, options.MaxCount, resolution, options.ResultPath, format);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
        }

        static void WrongFormat()
        {
            Console.WriteLine("Unknown result image format.");
            Environment.Exit(0);
        }
        
        static void SourceNotExist()
        {
            Console.WriteLine("Source file not found");
            Environment.Exit(0);
        }
        
        static void WrongResolution()
        {
            Console.WriteLine("Resolution must be positive.");
            Environment.Exit(0);
        }

        static ImageFormat TryGetImageFormat(string ext)
        {
            var type = typeof(ImageFormat);
            ext = ext[0].ToString().ToUpper() + ext[1..];
            var prop = type.GetProperty(ext);
            return (ImageFormat)prop?.GetValue(new object());
        }

        static TagCloud ConfigureTagCloud()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TxtFileReader>().As<IFileReader>();
            builder.RegisterType<RandomTagColor>().As<ITokenColorChooser>();
            builder.RegisterType<TokenOrdererByDescendingWeight>().As<ITokenOrderer>();
            builder.RegisterType<WordCounter>().As<ITokenWeigher>();
            builder.RegisterType<WordSelector>().UsingConstructor().As<IWordSelector>();
            builder.RegisterInstance(new CircularCloudMaker(Point.Empty)).As<ICloudMaker>();
            builder.RegisterType<FileReader>();
            builder.RegisterType<TokenGenerator>();
            builder.RegisterType<TagCloudMaker>();
            builder.RegisterType<TagCloudVisualiser>();
            builder.RegisterType<TagCloud>();
            var container = builder.Build();
            using var scope = container.BeginLifetimeScope();
            return scope.Resolve<TagCloud>();
        }
    }
}