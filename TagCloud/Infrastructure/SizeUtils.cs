using System.Drawing;
using System.Windows.Forms;

namespace TagCloud.Infrastructure
{
    public class SizeUtils
    {
        public static bool CanFillSizeWithSizes(
            Size mainRectangleSize, 
            Size innerRectangleSize,
            int innerRectangleCount)
        {
            var rectanglesInRow = mainRectangleSize.Width / innerRectangleSize.Width;
            var rectanglesInColumn = mainRectangleSize.Height / innerRectangleSize.Height;
            return rectanglesInRow * rectanglesInColumn >= innerRectangleCount;
        }

        public static Size GetWordBasedSize(string word, FontFamily fontFamily, float fontSize)
        {
            var font = new Font(fontFamily, fontSize);
            return TextRenderer.MeasureText(word, font);
        }
    }
}
