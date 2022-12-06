using System.Drawing;

namespace TagsCloudVisualization
{
    public static class Visualizator
    {
        public static void Save(this TagCloud tagCloud, string path)
        {
            var srcSize = tagCloud.GetScreenSize();


            var graphics = CreateGraphics(out var g, srcSize);
            var rectangles = tagCloud.GetRectangles();
            foreach (var textRectangle in rectangles)
            {
                //g.DrawRectangle(new Pen(Color.Black, 1),
                //    new Rectangle(textRectangle.rectangle.Location + srcSize / 2, textRectangle.rectangle.Size));
                var color = Color.FromArgb((int)textRectangle.font.Size % 255, 0,
                    (int)(textRectangle.font.Size * 2) % 255);
                g.DrawString(textRectangle.text, textRectangle.font, new SolidBrush(color),
                    textRectangle.rectangle.Location + new Size(srcSize.Width / 2, srcSize.Height / 2));
            }
            graphics.Save(path);

        }

        private static Bitmap CreateGraphics(out Graphics g, Size srcSize)
        {
            var bitmap =
                new Bitmap(srcSize.Width, srcSize.Height);
            g = Graphics.FromImage(bitmap);
            return bitmap;
        }
    }
}