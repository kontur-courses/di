using System.Drawing;

namespace TagCloud2.TextGeometry
{
    public class StringToRectangleConverter : IStringToSizeConverter
    {
        public Size Convert(string input, Font font)
        {
#pragma warning disable CA1416 // Validate platform compatibility
            var g = Graphics.FromImage(new Bitmap(100, 100));
            var s = g.MeasureString(input, font).ToSize();
            s.Width++;
            s.Height++;
            return s;
        }
    }
}
