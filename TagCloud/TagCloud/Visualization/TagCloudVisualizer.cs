using System.Drawing;

namespace TagCloud
{
    public class TagCloudVisualizer : IVisualizer
    {
        private readonly Pen[] pens;
        private readonly ITagCloud tagCloud;
        public string FontFamily => "Times New Roman";

        public TagCloudVisualizer(ITagCloud tagCloud)
        {
            this.tagCloud = tagCloud;
            pens = new Pen[7];
            pens[0] = new Pen(Color.Red);
            pens[1] = new Pen(Color.Orange);
            pens[2] = new Pen(Color.Yellow);
            pens[3] = new Pen(Color.Green);
            pens[4] = new Pen(Color.Teal);
            pens[5] = new Pen(Color.Blue);
            pens[6] = new Pen(Color.Purple);
        }

        public Bitmap CreateBitMap(int width, int height)
        {
            var bitMap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bitMap);
            var penNumber = 0;

            foreach (var wordRectangle in tagCloud.WordRectangles)
            {
                var pen = pens[penNumber];
                var word = wordRectangle.Word;
                var rectangle = wordRectangle.Rectangle;
                var font = GetBiggestFont(wordRectangle, FontFamily);
                graphics.DrawString(word, font, new SolidBrush(pen.Color),
                    new PointF(rectangle.Left,rectangle.Y));
                graphics.DrawPolygon(pen, RectangleToPointFArray(rectangle));
                penNumber++;
                penNumber %= pens.Length;
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