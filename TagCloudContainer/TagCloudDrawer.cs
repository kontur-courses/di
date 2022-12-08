using System.Drawing;

namespace TagsCloudVisualization
{
    public static class TagCloudDrawer
    {
        public static void DrawWithSave(List<TextRectangle> rectangles, string path)
        {
            var maxLocationX = GetMaxPointLocation(rectangles, x => x.rectangle.X);
            var maxLocationY = GetMaxPointLocation(rectangles, y => y.rectangle.Y);
            var srcSize = new Size(Math.Abs(maxLocationX.rectangle.Location.X * 3), Math.Abs(maxLocationY.rectangle.Location.Y * 3));
            var bitmap = new Bitmap(srcSize.Width, srcSize.Height);
            var g = Graphics.FromImage(bitmap);
            foreach (var textRectangle in rectangles)
            {
                var color = Color.FromArgb((int)textRectangle.font.Size % 255, 0,
                    (int)(textRectangle.font.Size * 2) % 255);
                g.DrawString(textRectangle.text, textRectangle.font, new SolidBrush(color),
                    textRectangle.rectangle.Location + new Size(srcSize.Width / 2, srcSize.Height / 2));
            }
            bitmap.Save(path);
        }

        private static TextRectangle GetMaxPointLocation(IEnumerable<TextRectangle> rectangles, Func<TextRectangle, int> func)
        {
            return rectangles.MaxBy(func);
        }
    }
}