using System.Drawing;
using System.Drawing.Drawing2D;
using TagsCloudTextProcessing;

namespace TagsCloudVisualization.Styling.TagSizeCalculators
{
    public abstract class TagSizeCalculator
    {
        public SizeF GetTagSize(FontProperties fontProperties, float wordSize, Token word)
        {
            SizeF result;
            var font = new Font(fontProperties.Name, wordSize);
            using (var image = new Bitmap(1000, 1000))
            {
                using (var graphics = Graphics.FromImage(image))
                {
                    using (var path = new GraphicsPath())
                    {
                        path.AddString(word.Word, font.FontFamily, (int) font.Style,
                            graphics.DpiY * font.Size / 72, new Point(0, 0), StringFormat.GenericTypographic);
                        result = path.GetBounds().Size;
                    }
                }
            }

            return result;
        }

        public abstract float GetScaleFactor(int tagCount, int minFontSize);
    }
}