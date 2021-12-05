using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization.Layouter
{
    public class Visualization : IDisposable
    {
        public List<RectangleWithWord> RectangleList { get;}

        private Pen ColorPen { get; }


        public Visualization(List<RectangleWithWord> rectangleWithWordsList, Pen colorPen)
        {
            RectangleList = rectangleWithWordsList;
            ColorPen = colorPen;
        }

        public void DrawAndSaveImage(Size imageSize, string path, ImageFormat format)
        {
            var font = new Font("Times", 15);
            using (var image = new Bitmap(imageSize.Width, imageSize.Height))
            {
                var drawImage = DrawRectangles(image, font);
                drawImage.Save(path, format);
            }
        }

        private Bitmap DrawRectangles(Bitmap image, Font font)
        {
            using (var graphics = Graphics.FromImage(image))
            {
                foreach (var rectangle in RectangleList)
                {
                    graphics.DrawRectangle(ColorPen, rectangle.RectangleElement);
                    graphics.DrawString(rectangle.WordElement.WordText, font, Brushes.White, 
                        rectangle.RectangleElement.Location.X, rectangle.RectangleElement.Location.Y);
                }

                return image;
            }
        }

        public void Dispose()
        {
            ColorPen.Dispose();
        }
    }
}
