using System;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.Curves;

namespace TagsCloudVisualization
{
    public class CloudParametersParser : ICloudParametersParser
    {
        public CloudParameters Parse(string[] input)
        {
            ICurve curve = null;
            string fontName = null;
            Color? color = null;
            Size? imageSize = null;
            string filePath = null;

            for (var i = 0; i < input.Length; i++)
                switch (input[i])
                {
                    case "-color":
                        color = GetColor(input, i);
                        break;
                    case "-curve":
                        curve = GetCurve(input, i);
                        break;
                    case "-fontName":
                        fontName = input[i + 1];
                        break;
                    case "-imageSize":
                        imageSize = GetImageSize(input, i);
                        break;
                    case "-filePath":
                        filePath = input[i + 1];
                        break;
                }

            return new CloudParameters
            {
                FontName = fontName,
                Curve = curve,
                Color = color,
                ImageSize = imageSize,
                FilePath = filePath
            };
        }

        private Size? GetImageSize(string[] args, int position)
        {
            var delimiter = args[position + 1].IndexOf('x');
            int.TryParse(args[position + 1].Substring(0, delimiter), out var width);
            int.TryParse(args[position + 1].Substring(delimiter + 1), out var height);
            return new Size(width, height);
        }

        private Color? GetColor(string[] args, int position)
        {
            return Color.FromName(args[position + 1]);
        }

        private ICurve GetCurve(string[] args, int position)
        {
            ICurve curve = null;
            switch (args[position + 1])
            {
                case "spiral":
                    curve = new Spiral(0.2, Math.PI / 36);
                    break;
                case "heart":
                    curve = new Heart(0.2, Math.PI / 36);
                    break;
                case "astroid":
                    curve = new Astroid(0.2, Math.PI / 36);
                    break;
            }

            return curve;
        }
    }
}