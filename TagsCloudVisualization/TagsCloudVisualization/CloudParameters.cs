using System;
using System.Drawing;
using TagsCloudVisualization.Curves;

namespace TagsCloudVisualization
{
    public class CloudParameters
    {
        public Size? ImageSize { get; set; }
        public Color? Color { get; set; }
        public string FontName { get; set; }
        public ICurve Curve { get; set; }

        public bool IsCorrect()
        {
            if (Curve == null)
            {
                Console.WriteLine(
                    "Error in the name of the curve. You need to choose one of them: spiral | heart | astroid");
                return false;
            }

            if (Color == null)
            {
                Console.WriteLine("Error in the name of the color. Choose simple color, in example red");
                return false;
            }

            if (ImageSize == null)
            {
                Console.WriteLine("Error in imageSize. Format input is numberxnumber, in example 800x600");
                return false;
            }

            if (FontName == null)
            {
                Console.WriteLine("Error in fontName. Choose simple fontName, in example arial");
                return false;
            }

            return true;
        }
    }
}