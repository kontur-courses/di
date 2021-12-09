using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualizationDI.Visualization;

namespace TagsCloudVisualizationDI.Layouter
{
    public class DefaultVisualization : IDisposable, IVisualization
    {
        private List<RectangleWithWord> Elementslist { get; }
        private Pen ColorPen { get; }
        private Brush ColorBrush { get; }
        private Font TextFont { get; }

        private ImageFormat Format { get; }

        private string SavePath { get; }
        private Size ImageSize { get; }



        public DefaultVisualization(List<RectangleWithWord> rectangleWithWordsList, string savePath)
        {
            Elementslist = rectangleWithWordsList;
            ColorPen = new Pen(Color.White, 10);
            ColorBrush = new SolidBrush(Color.White);
            TextFont = new Font("Times", 15);
            Format = ImageFormat.Jpeg;
            SavePath = savePath;
            ImageSize = new Size(5000, 5000);
        }

        public void DrawAndSaveImage()
        {
            using (var image = new Bitmap(ImageSize.Width, ImageSize.Height))
            {
                var drawImage = DrawRectangles(image);
                drawImage.Save(SavePath, Format);
            }
        }

        private Bitmap DrawRectangles(Bitmap image)
        {
            using (var graphics = Graphics.FromImage(image))
            {
                foreach (var element in Elementslist)
                {
                    //graphics.DrawRectangle(ColorPen, element.RectangleElement);

                    var fontSize = TextFont.Size + 3 * element.WordElement.CntOfWords;
                    var font = new Font("Times", fontSize);

                    graphics.DrawString(element.WordElement.WordText, font, ColorBrush, 
                        element.RectangleElement.Location.X, element.RectangleElement.Location.Y);
                }

                return image;
            }
        }

        public Size GetStringSize(RectangleWithWord word)
        {
            //using (var graphics = Graphics.FromImage(image))
            var image = new Bitmap(ImageSize.Width, ImageSize.Height);
            using (var graphics = Graphics.FromImage(image))
            {
                var fontSize = TextFont.Size + 3 * word.WordElement.CntOfWords;
                var font = new Font("Times", fontSize);

                var stringSize = graphics.MeasureString(word.WordElement.WordText, font);

                var rectSize = new Size((int)stringSize.Width, (int)stringSize.Height);

                return rectSize;
            }
        }

        public void Dispose()
        {
            ColorPen.Dispose();
            ColorBrush.Dispose();
            TextFont.Dispose();
        }
    }
}
