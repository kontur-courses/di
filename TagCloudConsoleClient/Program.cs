using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Autofac;
using CommandLine;
using TagsCloudVisualization;
using TagsCloudVisualization.Default;

namespace TagCloudConsoleClient
{
    public class Program
    {
        private static readonly Dictionary<string, ITokenOrderer> Orders = new Dictionary<string, ITokenOrderer>()
        {
            ["N"] = new TokenNonOrderer(),
            ["S"] = new TokenSortedOrder(),
            ["M"] = new TokenShuffler()
        };
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(Start);
        }

        private static void Start(Options options)
        {
            var ext = Path.GetExtension(options.ResultPath)?.TrimStart('.');
            var format = TryGetImageFormat(ext);
            if (options.Height < 1 || options.Width < 1)
            {
                WrongResolution();
                return;
            }
            if (ext == null || format == null)
            {
                WrongFormat();
                return;
            }
            var font = new Font(options.Font, 10);
            var source = new FileInfo(options.SourcePath);
            if (!source.Exists)
            {
                SourceNotExist(); 
                return;
            }
            var tagCloud = ConfigureTagCloud(options.Manhattan, options.Order);
            var resolution = new Size(options.Width, options.Height);
            try
            {
                tagCloud.CreateTagCloudFromFile(source, font, options.MaxCount, resolution, options.ResultPath, format);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            if (options.OpenResult)
                Process.Start(new ProcessStartInfo(options.ResultPath)
                { UseShellExecute = true });
        }

        private static void WrongFormat()
        {
            Console.WriteLine("Unknown result image format.");
        }

        private static void SourceNotExist()
        {
            Console.WriteLine("Source file not found");
        }

        private static void WrongResolution()
        {
            Console.WriteLine("Resolution must be positive.");
        }

        private static ImageFormat TryGetImageFormat(string ext)
        {
            var type = typeof(ImageFormat);
            ext = ext[0].ToString().ToUpper() + ext[1..];
            var prop = type.GetProperty(ext);
            return (ImageFormat)prop?.GetValue(new object());
        }

        private static TagCloud ConfigureTagCloud(bool manhattan, string order)
        {
            Func<PointF, PointF, double> metric;
            if (manhattan)
                metric = CircularCloudMaker.ManhattanDistance;
            else
                metric = CircularCloudMaker.Distance;
            var orderer = Orders["N"];
            if (order != null && Orders.ContainsKey(order))
                orderer = Orders[order];
            var builder = new ContainerBuilder();
            builder.RegisterType<TxtFileReader>().As<IFileReader>();
            builder.RegisterType<RandomTagColor>().As<ITokenColorChooser>();
            builder.RegisterInstance(orderer).As<ITokenOrderer>();
            builder.RegisterType<WordCounter>().As<ITokenWeigher>();
            builder.RegisterType<WordSelector>().UsingConstructor().As<IWordSelector>();
            builder.RegisterInstance(new CircularCloudMaker(Point.Empty, metric)).As<ICloudMaker>();
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