﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using TagsCloudVisualization.ColorGenerators;
using TagsCloudVisualization.TagsCloudDrawer;
using TagsCloudVisualization.TagsCloudDrawer.TagsCloudDrawerSettingsProvider;

// Disable warning https://docs.microsoft.com/ru-ru/dotnet/fundamentals/code-analysis/quality-rules/ca1416
// as several methods use windows api
#pragma warning disable CA1416

namespace TagsCloudVisualization.Tests.CloudLayouter
{
    public class CircularCloudLayouterTestsLogger
    {
        private readonly RectanglesCloudDrawer _drawer = new(new TagsCloudDrawerSettingsProvider
        {
            ColorGenerator = new RainbowColorGenerator(new Random())
        });

        private readonly Size _imageSize = new(1000, 1000);
        private string _outputDirectory;

        public void Log(IEnumerable<Rectangle> rectangles, string testName)
        {
            if (string.IsNullOrEmpty(_outputDirectory))
                throw new Exception($"{nameof(_outputDirectory)} was null or empty");
            var path = Path.Combine(_outputDirectory, testName + ".bmp");
            using var image = new Bitmap(_imageSize.Width, _imageSize.Height);
            _drawer.Draw(image, rectangles.Select(Tag.FromRectangle));
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