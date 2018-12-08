using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.ResultFormatters
{
    public class CircularCloudLayouterResultFormatter : IResultFormatter
    {
        public void GenerateResult(Size size, FontFamily fontFamily, Brush brush, string outputFileName,
            Dictionary<string, (Rectangle, int)> rectangles)
        {
            using (var bitmap = new Bitmap(size.Width, size.Height))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    foreach (var entry in rectangles)
                    {
                        var font = new Font(fontFamily, 10);
                        var generatedFont = GetFont(graphics, entry.Key, entry.Value.Item1.Size, font);

                        graphics.DrawString(entry.Key, generatedFont, brush, entry.Value.Item1);

                    }
                    bitmap.Save(outputFileName);
                }
            }
        }

        private Font GetFont(Graphics g, string longString, Size room, Font preferredFont)
        {
            var realSize = g.MeasureString(longString, preferredFont);
            var heightScaleRatio = room.Height / realSize.Height;
            var widthScaleRatio = room.Width / realSize.Width;

            var scaleRatio = heightScaleRatio < widthScaleRatio ? heightScaleRatio : widthScaleRatio;

            var scaleFontSize = preferredFont.Size * scaleRatio;

            return new Font(preferredFont.FontFamily, scaleFontSize - 1 > 0 ? scaleFontSize - 1 : scaleFontSize);
        }
    }
}
