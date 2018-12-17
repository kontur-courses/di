using System.Drawing;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Layout;

namespace TagsCloudContainer.Drawing
{
    public class ImageDrawer : IDrawer
    {
        public Bitmap Draw(WordLayout layout, ImageSettings settings, out Graphics graphics)
        {
            var bitmap = new Bitmap(settings.Size.Width, settings.Size.Height);
            graphics = Graphics.FromImage(bitmap);

            graphics.FillRectangle(new SolidBrush(settings.BackgroundColor), new Rectangle(new Point(0, 0), settings.Size));

            foreach (var pair in layout.WordRectangles)
            {
                var rectangle = pair.Value.Item1;
                var font = settings.TextFont.SetSize(pair.Value.Item2);
               
                graphics.DrawRectangle(Pens.Blue, rectangle);
                graphics.DrawString(pair.Key, font, new SolidBrush(settings.TextColor), rectangle);
            }

            return bitmap;
        }
    }
}