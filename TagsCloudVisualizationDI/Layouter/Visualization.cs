using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualizationDI.Layouter
{
    public class Visualization : IDisposable
    {
        private List<RectangleWithWord> RectangleList { get; }
        private Pen ColorPen { get; }
        private Brush ColorBrush { get; }
        private Font TextFont { get; }


        public Visualization(List<RectangleWithWord> rectangleWithWordsList, Pen colorPen, Brush colorBrush, Font textFont)
        {
            RectangleList = rectangleWithWordsList;
            ColorPen = colorPen;
            ColorBrush = colorBrush;
            TextFont = textFont;
        }

        public void DrawAndSaveImage(Size imageSize, string path, ImageFormat format)
        {
            using (var image = new Bitmap(imageSize.Width, imageSize.Height))
            {
                var drawImage = DrawRectangles(image);
                drawImage.Save(path, format);
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
