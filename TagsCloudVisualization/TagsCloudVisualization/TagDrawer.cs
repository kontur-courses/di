using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public static class TagDrawer
    {
        public static void Draw(string fileName, CircularCloudLayouter layouter)
        {
            var pens = new List<Pen>
            {
                new Pen(new SolidBrush(Color.Blue)),
                new Pen(new SolidBrush(Color.Green)),
                new Pen(new SolidBrush(Color.Black)),
                new Pen(new SolidBrush(Color.Red)),
                new Pen(new SolidBrush(Color.Yellow)),
                new Pen(new SolidBrush(Color.Magenta))
            };
            const int border = 10;
            var width = layouter.SizeOfCloud.Width + border;
            var height = layouter.SizeOfCloud.Height + border;
            var bitmap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bitmap);

            graphics.Clear(Color.White);
            graphics.TranslateTransform(border / 2 - layouter.LeftDownPointOfCloud.X,
                border / 2 - layouter.LeftDownPointOfCloud.Y);

            var colorIndex = 0;
            layouter.Rectangles.ForEach(rect =>
            {
                colorIndex = (colorIndex + 1) % pens.Count;
                graphics.DrawRectangle(pens[colorIndex], rect);
            });
  
            bitmap.Save(fileName);
        }
    }
}