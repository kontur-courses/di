using System.Drawing;

namespace TagCloud
{
    public class TagCloudVisualizer : IVisualizer
    {
        private readonly Color[] colors;
        private readonly ITagCloud tagCloud;
        public string FontFamily => "Times New Roman";

        public TagCloudVisualizer(ITagCloud tagCloud)
        {
            this.tagCloud = tagCloud;
            colors = new Color[7];
            colors[0] = Color.Red;
            colors[1] = Color.Orange;
            colors[2] = Color.Yellow;
            colors[3] = Color.Green;
            colors[4] = Color.Teal;
            colors[5] = Color.Blue;
            colors[6] = Color.Purple;
        }

        public Bitmap CreateBitMap(int width, int height)
        {
            var bitMap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bitMap);
            var colorNumber = 0;

            foreach (var wordRectangle in tagCloud.WordRectangles)
            {
                var color = colors[colorNumber];
                var word = wordRectangle.Word;
                var rectangle = wordRectangle.Rectangle;
                var font = GetBiggestFont(wordRectangle, FontFamily);
                graphics.FillPolygon(new SolidBrush(Color.Black), RectangleToPointFArray(rectangle));
                graphics.DrawString(word, font, new SolidBrush(color),
                    new PointF(rectangle.Left, rectangle.Y));
                graphics.DrawPolygon(new Pen(color), RectangleToPointFArray(rectangle));

                colorNumber++;
                colorNumber %= colors.Length;
            }

            return bitMap;
        }

        private Font GetBiggestFont(WordRectangle token, string fontFamilyName)
        {
            var word = token.Word;
            var rectangle = token.Rectangle;
            var fontSize = 10;
            for (; fontSize < 50; fontSize++)
            {
                var font = new Font(fontFamilyName, fontSize);
                if (font.Height > rectangle.Height || font.Size * word.Length > rectangle.Width)
                    break;
            }

            return new Font(fontFamilyName, fontSize - 1);
        }

        private static PointF[] RectangleToPointFArray(Rectangle rectangle)
        {
            return new[]
            {
                new PointF(rectangle.Left, rectangle.Bottom),
                new PointF(rectangle.Right, rectangle.Bottom),
                new PointF(rectangle.Right, rectangle.Top),
                new PointF(rectangle.Left, rectangle.Top)
            };
        }
    }
}