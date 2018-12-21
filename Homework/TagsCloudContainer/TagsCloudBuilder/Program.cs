using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Autofac;
using CommandLine;
using TagsCloudBuilder.Drawer;
using TagsCloudContainer;
using TagsCloudContainer.ColorAlgorithm;
using TagsCloudContainer.WordsFilter;
using TagsCloudContainer.WordsFilter.BannedWords;
using TagsCloudVisualization;
using TagsCloudVisualization.CloudLayouter;

namespace TagsCloudBuilder
{
    public class Program
    {
        private static Dictionary<string, ImageFormat> imageFormatsComparer = new Dictionary<string, ImageFormat>()
        {
            { "png", ImageFormat.Png},
            {"jpeg", ImageFormat.Jpeg },
            { "bmp", ImageFormat.Bmp}
        };

        private static Dictionary<string, IColorAlgorithm> colorAlgorithmComparer =
            new Dictionary<string, IColorAlgorithm>()
            {
                { "random", new RandomColorAlgorithm() },
                { "static", new StaticColorAlgorithm() },
                { "frequency", new FrequencyColorAlgorithm() }
            };

        private class Options
        {
            [Option('i', "input", Default = "text.txt",
                HelpText = "Filename with input data.")]
            public string InputFilename { get; set; }

            [Option('d', "debug", Required = false, Default = false,
                HelpText = "Draw area around every word.")]
            public bool Debug { get; set; }

            [Option('s', "max font size", Required = false, Default = 50,
                HelpText = "Set the max size of the words in px. Can't be less then 10px.")]
            public int MaxFontSize { get; set; }

            [Option('f', "font", Required = false, Default = "Arial",
                HelpText = "Set the font for text.")]
            public string FontFamily { get; set; }

            [Option('b', "boring words", Default = "boring.txt",
                HelpText = "Filename with ignored words.")]
            public string BannedWordsFilename { get; set; }

            [Option('s', "canvas size", Default = new[] { 2000, 2000 },
                HelpText = "Set the canvas size of output file.")]
            public int[] CanvasSize { get; set; }

            [Option('o', "output filename", Default = "sample.png",
                HelpText = "Set the output file name.")]
            public string OutputFilename { get; set; }

            [Option('c', "center point", Default = new[] { 750, 1000 },
                HelpText = "Set the center of clouds.")]
            public int[] CenterPoint { get; set; }

            [Option('r', "radius step", Default = 0.00001,
                HelpText = "Set the radius step.")]
            public double RadiusStep { get; set; }

            [Option('a', "angle step", Default = 0.01,
                HelpText = "Set the angle step.")]
            public double AngleStep { get; set; }

            [Option('l', "words length", Default = new[] { 5, int.MaxValue },
                HelpText = "Set the bounds of words length")]
            public int[] WordsLength { get; set; }

            [Option('e', "output file extension", Default = "png", HelpText = "Set the output file extension.")]
            public string OutputFileExtension { get; set; }

            [Option('a', "color algorithm", Default = "frequency", HelpText = "Set the color algorithm chooser.")]
            public string ColorAlgorithmName { get; set; }
        }

        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(BuildAndRun);
        }

        private static void BuildAndRun(Options options)
        {
            using (var container = BuildContainer(options))
            {
                var drawer = container.Resolve<IDrawer>();
                drawer.DrawAndSaveWords();
            }
        }

        private static IContainer BuildContainer(Options options)
        {
            var builder = new ContainerBuilder();
            var imageFormat = GetImageFormat(options.OutputFileExtension);

            builder.RegisterType<TxtWordsPreparer>()
                .As<IWordsPreparer>()
                .WithParameter("fileName", options.InputFilename);

            builder.RegisterType<BoringWords>()
                .As<IBoringWords>()
                .WithParameter("fileName", options.BannedWordsFilename);

            builder.RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>()
                .WithParameter("center", new Point(options.CenterPoint[0], options.CenterPoint[1]))
                .WithParameter("radiusStep", options.RadiusStep)
                .WithParameter("angleStep", options.AngleStep);

            builder.RegisterType<ContainersCreator>()
                .As<IContainersCreator>()
                .WithParameter("fontName", options.FontFamily)
                .WithParameter("maxFontSize", options.MaxFontSize)
                .WithParameter("colorAlgorithm", GetColorAlgorithm(options.ColorAlgorithmName));

            if (options.Debug)
                builder.RegisterType<DebugDrawer>()
                    .As<IDrawer>()
                    .WithParameter("canvasSize", new Size(options.CanvasSize[0], options.CanvasSize[1]))
                    .WithParameter("fileName", options.OutputFilename)
                    .WithParameter("imageFormat", imageFormat);
            else
                builder.RegisterType<Drawer.Drawer>()
                    .As<IDrawer>()
                    .WithParameter("canvasSize", new Size(options.CanvasSize[0], options.CanvasSize[1]))
                    .WithParameter("fileName", options.OutputFilename)
                    .WithParameter("imageFormat", imageFormat);

            builder.RegisterType<FilteredWords>()
                .As<IFilteredWords>()
                .WithParameter("leftBound", options.WordsLength[0])
                .WithParameter("rightBound", options.WordsLength[1]);

            builder.RegisterType<SpiralInfo>()
                .AsSelf();

            return builder.Build();
        }

        private static ImageFormat GetImageFormat(string formatName)
        {
            var lowerFormatName = formatName.ToLower();
            if (!imageFormatsComparer.ContainsKey(lowerFormatName))
                return ImageFormat.Png;

            return imageFormatsComparer[lowerFormatName];
        }

        private static IColorAlgorithm GetColorAlgorithm(string colorAlgorithmName)
        {
            if (colorAlgorithmComparer.ContainsKey(colorAlgorithmName.ToLower()))
                return colorAlgorithmComparer[colorAlgorithmName];

            return colorAlgorithmComparer["random"];
        }
    }
}
