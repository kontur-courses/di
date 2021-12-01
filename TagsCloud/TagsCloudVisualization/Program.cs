using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TagsCloudVisualization.ColorGenerators;

// Disable warning https://docs.microsoft.com/ru-ru/dotnet/fundamentals/code-analysis/quality-rules/ca1416
// as several methods use windows api
#pragma warning disable CA1416

namespace TagsCloudVisualization
{
    internal class Program
    {
        private static readonly Size GeneratedImageSize = new(1000, 1000);

        private static void Main(string[] args)
        {
            var directory = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedClouds");
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            foreach (var generator in GetGenerators())
            {
                using var bitmap = new Bitmap(GeneratedImageSize.Width, GeneratedImageSize.Height);
                generator.Generate(bitmap);
                var filename = GenerateFileName();
                var path = Path.Combine(directory, filename + ".bmp");
                bitmap.Save(path, ImageFormat.Bmp);
            }
        }

        private static IEnumerable<TagsCloudGenerator> GetGenerators()
        {
            var rnd = new Random();
            yield return new TagsCloudGenerator(
                50,
                new CircularCloudLayouter(new Point()),
                () => new Size(rnd.Next(30, 50), rnd.Next(20, 30)),
                new TagsCloudDrawer(Color.Gray, new RandomColorGenerator(rnd)));
            yield return new TagsCloudGenerator(
                100,
                new CircularCloudLayouter(new Point()),
                () => new Size(rnd.Next(40, 50), rnd.Next(20, 30)),
                new TagsCloudDrawer(Color.Gray, new RainbowColorGenerator(rnd)));
            yield return new TagsCloudGenerator(
                500,
                new CircularCloudLayouter(new Point()),
                () => new Size(rnd.Next(40, 50), rnd.Next(20, 30)),
                new TagsCloudDrawer(Color.Gray, new RainbowColorGenerator(rnd)));
            yield return new TagsCloudGenerator(
                500,
                new CircularCloudLayouter(new Point()),
                () => new Size(rnd.Next(10, 50), rnd.Next(10, 50)),
                new TagsCloudDrawer(Color.Gray, new GrayscaleColorGenerator(rnd)));
        }

        private static string GenerateFileName() => DateTime.Now.Ticks.ToString();
    }
}
#pragma warning restore CA1416