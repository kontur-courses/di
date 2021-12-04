using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TagsCloudVisualization.ColorGenerators;
using TagsCloudVisualization.ImageCreator;
using TagsCloudVisualization.ImageSavior;
using TagsCloudVisualization.TagsCloudDrawer;

// Disable warning https://docs.microsoft.com/ru-ru/dotnet/fundamentals/code-analysis/quality-rules/ca1416
// as several methods use windows api
#pragma warning disable CA1416

namespace TagsCloudVisualization.Tests.CloudLayouter
{
    public class CircularCloudLayouterTestsLogger
    {
        private readonly IDrawer _drawer = new Drawer();
        private readonly IColorGenerator _colorGenerator = new RainbowColorGenerator(new Random());

        private readonly IImageSettingsProvider _imageSettingsProvider = new ImageSettingsProvider
        {
            ImageSize = new Size(1000, 1000)
        };

        private readonly IImageSavior _savior = new PngSavior();
        private string _outputDirectory;

        public void Log(IEnumerable<Rectangle> rectangles, string testName)
        {
            if (string.IsNullOrEmpty(_outputDirectory))
                throw new Exception($"{nameof(_outputDirectory)} was null or empty");
            var path = Path.Combine(_outputDirectory, testName);
            var creator = new ImageCreator.ImageCreator(_drawer, _savior, _imageSettingsProvider);
            creator.Create(path, rectangles.Select(rect => new RectangleDrawable(rect, _colorGenerator)));
            Console.WriteLine($"Tag cloud visualization saved to file {path}");
        }

        public void Init(string outputDirectory)
        {
            _outputDirectory = outputDirectory;
            RecreateDirectory(outputDirectory);
        }

        private static void RecreateDirectory(string directory)
        {
            if (Directory.Exists(directory))
            {
                var directoryInfo = new DirectoryInfo(directory);
                foreach (var file in directoryInfo.GetFiles()) file.Delete();
            }
            else
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
#pragma warning restore CA1416
}