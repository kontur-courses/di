using System.Drawing;
using TagsCloudContainer.Geom;

namespace TagsCloudContainer.Drawing
{
    public class ImageDrawer : IDrawer
    {
        public Bitmap Draw(CircularCloudLayouter layouter, int imageWidth=1024, int imageHeight=1024)
        {
            var image = new Bitmap(imageWidth, imageHeight);
            var graphics = Graphics.FromImage(image);

            foreach (var rectangle in layouter.Rectangles)
            {
                graphics.FillRectangle(Brushes.LightCoral, rectangle);
                graphics.DrawRectangle(Pens.Black, rectangle);
            }

            return image;
        }
    }
}
