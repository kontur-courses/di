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
        //private Pen ColorPen { get; }
        private Brush ColorBrush { get; }
        private Font TextFont { get; }

        private ImageFormat Format { get; }

        private string SavePath { get; }
        private Size ImageSize { get; }


        
        public DefaultVisualization(List<RectangleWithWord> rectangleWithWordsList, string savePath, 
            SolidBrush brush, Font font, ImageFormat imageFormat, Size imageSize)
        {
            Elementslist = rectangleWithWordsList;
            //ColorPen = pen;
            ColorBrush = brush;
            TextFont = font;
            Format = imageFormat;
            SavePath = savePath + '.'+ imageFormat;
            ImageSize = imageSize;
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
            //ColorPen.Dispose();
            ColorBrush.Dispose();
            TextFont.Dispose();
        }
    }
}
