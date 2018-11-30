using System;
using System.Drawing;
using TagsCloudVisualization.Curves;

namespace TagsCloudVisualization
{
    public class CloudParametersParser
    {
        public static CloudParameters Parse(string[] input)
        {
            ICurve curve = null;
            Font font = null;
            Color? color = null;
            Size? imageSize = null;

            for (var i = 0; i < input.Length; i++)
                switch (input[i])
                {
                    case "-color":
                        color = GetColor(input, i);
                        break;
                    case "-curve":
                        curve = GetCurve(input, i);
                        break;
                    case "-font":
                        font = GetFont(input, i);
                        break;
                    case "-imageSize":
                        imageSize = GetImageSize(input, i);
                        break;
                }

            return new CloudParameters
            {
                Font = font,
                Curve = curve,
                Color = color,
                ImageSize = imageSize
            };
        }

        private static Size? GetImageSize(string[] input, int i)
        {
            throw new NotImplementedException();
        }

        private static Font GetFont(string[] args, int position)
        {
            throw new NotImplementedException();
        }

        private static Color? GetColor(string[] args, int position)
        {
            throw new NotImplementedException();
        }

        private static ICurve GetCurve(string[] args, int position)
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