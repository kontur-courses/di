using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualizationDI.Visualization;

namespace TagsCloudVisualizationDI.Layouter
{
    public class DefaultVisualization : IDisposable, IVisualization
    {
        private List<RectangleWithWord> RectangleList { get; }
        private Pen ColorPen { get; }
        private Brush ColorBrush { get; }
        private Font TextFont { get; }

        private ImageFormat Format { get; }

        private string SavePath { get; }
        private Size ImageSize { get; }



        public DefaultVisualization(List<RectangleWithWord> rectangleWithWordsList, Pen colorPen, Brush colorBrush, 
            Font textFont, ImageFormat format, string savePath, Size imageSize)
        {
            RectangleList = rectangleWithWordsList;
            ColorPen = colorPen;
            ColorBrush = colorBrush;
            TextFont = textFont;
            Format = format;
            SavePath = savePath;
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
                foreach (var rectangle in RectangleList)
                {
                    graphics.DrawRectangle(ColorPen, rectangle.RectangleElement);
                    graphics.DrawString(rectangle.WordElement.WordText, TextFont, ColorBrush, 
                        rectangle.RectangleElement.Location.X, rectangle.RectangleElement.Location.Y);
                }

                return image;
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
