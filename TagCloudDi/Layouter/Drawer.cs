using System.Drawing;

namespace TagCloudDi.Layouter
{
    public class Drawer(Settings settings, RectangleGenerator rectangleGenerator)
    {
        public Image GetImage()
        {
            var image = new Bitmap(settings.ImageWidth, settings.ImageHeight);
            using (var gr = Graphics.FromImage(image))
            {
                gr.Clear(Color.Black);
                foreach (var rectangleData in rectangleGenerator.GetRectanglesData())
                    using (var font = new Font(settings.FontName, rectangleData.fontSize, FontStyle.Regular))
                        gr.DrawString(rectangleData.word, font, Brushes.Gainsboro, rectangleData.rectangle);
            }

            return image;
        }
    }
}
