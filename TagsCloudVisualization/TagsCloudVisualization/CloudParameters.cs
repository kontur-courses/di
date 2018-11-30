using System;
using System.Drawing;
using TagsCloudVisualization.Curves;

namespace TagsCloudVisualization
{
    public class CloudParameters
    {
        public Size? ImageSize { get; set; }
        public Color? Color { get; set; }
        public Font Font { get; set; }
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
                Console.WriteLine("#####");
                return false;
            }

            if (ImageSize == null)
            {
                Console.WriteLine("#####");
                return false;
            }

            if (Font == null)
            {
                Console.WriteLine("#####");
                return false;
            }

            return true;
        }
    }
}