using System.Drawing;
using TagCloudDi.Layouter;

namespace TagCloudDi.Drawer
{
    public class Drawer(Settings settings, IRectanglesGenerator rectanglesGenerator) : IDrawer
    {
        public Image GetImage()
        {
            var image = new Bitmap(settings.ImageWidth, settings.ImageHeight);
            using var gr = Graphics.FromImage(image);
            gr.Clear(Color.FromName(settings.BackColor));
            var brush = new SolidBrush(Color.FromName(settings.TextColor));
            foreach (var rectangleData in rectanglesGenerator.GetRectanglesData())
                using (var font = new Font(settings.FontName, rectangleData.fontSize, FontStyle.Regular))
                    gr.DrawString(rectangleData.word, font, brush, rectangleData.rectangle);

            return image;
        }
    }
}
