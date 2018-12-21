using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Autofac;
using TagsCloudBuilder.Drawer;
using TagsCloudContainer;
using TagsCloudContainer.ColorAlgorithm;
using TagsCloudContainer.WordsFilter;
using TagsCloudContainer.WordsFilter.BoringWords;
using TagsCloudVisualization;
using TagsCloudVisualization.CloudLayouter;

namespace TagsCloudBuilder
{
    public class TagCloudConsole
    {
        private static readonly Dictionary<string, ImageFormat> imageFormatsComparer = new Dictionary<string, ImageFormat>()
        {
            { "png", ImageFormat.Png},
            {"jpeg", ImageFormat.Jpeg },
            { "bmp", ImageFormat.Bmp}
        };

        private static readonly Dictionary<string, IColorAlgorithm> colorAlgorithmComparer =
            new Dictionary<string, IColorAlgorithm>()
            {
                { "random", new RandomColorAlgorithm() },
                { "static", new StaticColorAlgorithm() },
                { "frequency", new FrequencyColorAlgorithm() }
            };

        public static IContainer BuildContainer(Options options)
        {
            if (!File.Exists(options.InputFilename))
            {
                throw new IOException($"This file {options.InputFilename} doesn't exists. Check the correctness of file path.");
            }

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
            if (imageFormatsComparer.ContainsKey(lowerFormatName))
                return imageFormatsComparer[lowerFormatName];

            return ImageFormat.Png;
        }

        private static IColorAlgorithm GetColorAlgorithm(string colorAlgorithmName)
        {
            if (colorAlgorithmComparer.ContainsKey(colorAlgorithmName.ToLower()))
                return colorAlgorithmComparer[colorAlgorithmName];

            return colorAlgorithmComparer["random"];
        }
    }
}