using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Autofac;
using CommandLine;
using TagsCloud;

namespace TagCloudConsole
{
    public static class EntryPoint
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Run)
                .WithNotParsed(HandleErrors);
        }

        private static void Run(Options options)
        {
            if (!File.Exists(options.BoringWordsPath))
            {
                Console.WriteLine($"Incorrect input: {options.BoringWordsPath}");
                return;
            }

            if (!File.Exists(options.TextPath))
            {
                Console.WriteLine($"Incorrect input: {options.TextPath}");
                return;
            }

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<TagCloudLayouter>().As<ITagCloudLayouter>();
            containerBuilder.RegisterType<TagCloudRender>().AsSelf();
            containerBuilder.RegisterInstance(
                new BoringWordsFilter(
                    new LowerWord(
                        new WordsFromFile(options.BoringWordsPath)
                    ).ToLower(),
                    new LowerWord(
                        GetWordsFromFile(options)
                    ).ToLower()
                )
            ).As<IBoringWordsCollection>();
            containerBuilder.RegisterInstance(
                    new CoordinatesAtImage(
                        new Size(
                            options.Height, options.Width)))
                .AsSelf();
            containerBuilder.RegisterType<TagCloudLayouter>().AsSelf();
            containerBuilder.RegisterType<FrequencyCollection>().As<IFrequencyCollection>();
            var center = new Point(0, 0);
            var step = 0.01;
            var width = 0.1;
            containerBuilder.RegisterInstance(
                    new CircularCloudLayouter(
                        center, new CircularSpiral(center, width, step)))
                .As<ICloudLayouter>();
            containerBuilder.RegisterInstance(
                    new Picture(
                        new Size(options.Height, options.Width),
                        new FontFamily(options.FontFamily),
                        Color.FromName(options.Color),
                        GetImageFormat(options.Format), 
                        options.Name))
                .As<IGraphics>();
            
            using (var container = containerBuilder.Build())
            {
                var render = container.Resolve<TagCloudRender>();
                render.Render();
            }
        }

        private static IWordCollection GetWordsFromFile(Options options)
        {
            if (options.IsDocx)
                return new WordsFromMicrosoftWord(options.TextPath);
            return new WordsFromFile(options.TextPath);
        }

        private static void HandleErrors(IEnumerable<Error> errors)
        {
            Console.WriteLine(
                string.Join(
                    Environment.NewLine,
                    errors
                )
            );
        }

        private static ImageFormat GetImageFormat(string format)
        {
            var lowerFormat = format.ToLower();
            switch (lowerFormat)
            {
                case "png":
                    return ImageFormat.Png;
                case "bmp":
                    return ImageFormat.Bmp;
                case "jpeg":
                    return ImageFormat.Jpeg;
            }

            throw new ArgumentException($"Incorrect image format. Expected one of: png, jpeg, bmp, but was {format}");
        }
    }
}