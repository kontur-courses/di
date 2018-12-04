using System;
using System.Drawing;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.PointGenerators;

namespace TagsCloudVisualization
{
    public class CloudParametersParser : ICloudParametersParser
    {
        public CloudParameters Parse(Options options)
        {
            var color = Color.FromName(options.Color);
            var imageSize = GetImageSize(options.ImageSize);
            var pointGenerator = GetPointGenerator(options.PointGenerator);

            return new CloudParameters
            {
                FontName = options.FontName,
                PointGenerator = pointGenerator,
                Color = color,
                ImageSize = imageSize
            };
        }

        private Size GetImageSize(string input)
        {
            var delimiter = input.IndexOf('x');
            int.TryParse(input.Substring(0, delimiter), out var width);
            int.TryParse(input.Substring(delimiter + 1), out var height);
            return new Size(width, height);
        }

        private IPointGenerator GetPointGenerator(string input)
        {
            IPointGenerator pointGenerator = null;
            switch (input)
            {
                case "spiral":
                    pointGenerator = new Spiral(0.2, Math.PI / 36);
                    break;
                case "heart":
                    pointGenerator = new Heart(0.2, Math.PI / 36);
                    break;
                case "astroid":
                    pointGenerator = new Astroid(0.2, Math.PI / 36);
                    break;
            }

            return pointGenerator;
        }
    }
}