using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace TagsCloudVisualizationDI.TextAnalization.Visualization
{
    public class DefaultVisualization : IVisualization
    {
        private Brush ColorBrush { get; }
        private Font TextFont { get; }
        private Size ImageSize { get; }

        private int SizeMultiplier { get; }

        public DefaultVisualization(Brush brush, Font font, Size imageSize, int sizeMultiplier)
        {
            ColorBrush = brush;
            TextFont = font;
            ImageSize = imageSize;
            SizeMultiplier = sizeMultiplier;
        }

        public void DrawAndSaveImage(List<RectangleWithWord> elements, string savePath, ImageFormat format)
        {
            using var image = new Bitmap(ImageSize.Width, ImageSize.Height);
            var drawImage = DrawRectangles(image, elements);
            try
            {
                drawImage.Save(savePath, format);
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
                throw new FileNotFoundException($"Invalid save path: {savePath}");
            }
        }


        private Bitmap DrawRectangles(Bitmap image, List<RectangleWithWord> elementList)
        {
            using var graphics = Graphics.FromImage(image);
            foreach (var element in elementList)
            {
                var fontSize = SizeMultiplier * element.WordElement.CntOfWords;
                var font = new Font("Times", fontSize);

                graphics.DrawString(element.WordElement.WordText, font, ColorBrush,
                    element.RectangleElement.Location.X, element.RectangleElement.Location.Y);
            }

            return image;
        }

        public void Dispose()
        {
            ColorBrush.Dispose();
            TextFont.Dispose();
        }

        public List<RectangleWithWord> FindSizeForElements(Dictionary<string, RectangleWithWord> formedElements)
        {
            var result = new List<RectangleWithWord>();

            foreach (var element in formedElements.Values)
            {
                using var image = new Bitmap(ImageSize.Width, ImageSize.Height);
                using var graphics = Graphics.FromImage(image);


                var fontSize = element.WordElement.CntOfWords * SizeMultiplier;
                var font = new Font("Times", fontSize);


                var newSize = graphics.MeasureString(element.WordElement.WordText, font);
                var sizedElement = new RectangleWithWord(new Rectangle
                        (element.RectangleElement.Location, newSize.ToSize()),
                    element.WordElement);
                result.Add(sizedElement);
            }

            return result;
        }
    }
}
