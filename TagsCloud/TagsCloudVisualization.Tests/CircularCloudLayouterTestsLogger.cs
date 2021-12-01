using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TagsCloudVisualization.ColorGenerators;

// Disable warning https://docs.microsoft.com/ru-ru/dotnet/fundamentals/code-analysis/quality-rules/ca1416
// as several methods use windows api
#pragma warning disable CA1416

namespace TagsCloudVisualization.Tests
{
    public class CircularCloudLayouterTestsLogger
    {
        private readonly TagsCloudDrawer _drawer = new(Color.Gray, new RainbowColorGenerator(new Random()));
        private readonly Size _imageSize = new(1000, 1000);
        private string _outputDirectory;

        public void Log(IEnumerable<Rectangle> rectangles, string testName)
        {
            if (string.IsNullOrEmpty(_outputDirectory))
                throw new Exception($"{nameof(_outputDirectory)} was null or empty");
            var path = Path.Combine(_outputDirectory, testName + ".bmp");
            using var image = new Bitmap(_imageSize.Width, _imageSize.Height);
            _drawer.Draw(image, rectangles);
            image.Save(path, ImageFormat.Bmp);
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