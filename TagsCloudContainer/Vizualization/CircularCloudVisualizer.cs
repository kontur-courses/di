using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class CircularCloudVisualizer
    {
        private readonly Pen rectangleBorderPen = new Pen(Color.Black);
        private readonly Brush rectangleFillBrush = new SolidBrush(Color.Gray);
        private readonly Brush backgroundFillBrush = new SolidBrush(Color.White);
        private readonly Brush textBrush = new SolidBrush(Color.Blue);

        public Bitmap VisualizeLayout(CircularCloudLayouter layouter)
        {
            var rectangles = layouter.GetRectangles();
            var imageSize = GetImageSize(rectangles);
            if (imageSize.Width == 0 && imageSize.Height == 0)
            {
                return null;
            }

            return GetImage(imageSize, rectangles);
        }

        private static Size GetImageSize(List<Rectangle> rectangles)
        {
            var cloudRightBorder = rectangles.Max(rect => rect.Right);
            var cloudBottomBorder = rectangles.Max(rect => rect.Bottom);
            var cloudLeftBorder = rectangles.Min(rect => rect.Left);
            var cloudTopBorder = rectangles.Min(rect => rect.Top);
            return new Size(cloudRightBorder + cloudLeftBorder, cloudBottomBorder + cloudTopBorder);
        }

        private Bitmap GetImage(Size imageSize, List<Rectangle> rectangles)
        {
            var image = new Bitmap(imageSize.Width, imageSize.Height);

            using (var graphics = Graphics.FromImage(image))
            {
                FillBackground(graphics, imageSize, backgroundFillBrush);

                var rectangleNumber = 0;
                foreach (var rectangle in rectangles)
                {
                    rectangleNumber++;
                    graphics.FillRectangle(rectangleFillBrush, rectangle);
                    graphics.DrawRectangle(rectangleBorderPen, rectangle);
                    graphics.DrawString(
                        rectangleNumber.ToString(),
                        new Font(FontFamily.GenericSansSerif, rectangle.Height / 3),
                        textBrush,
                        rectangle);
                }
            }

            return image;
        }

        private static void FillBackground(Graphics graphics, Size imageSize, Brush backgroundBrush)
        {
            var backgroundRectangle = new Rectangle(0, 0, imageSize.Width, imageSize.Height);
            graphics.FillRectangle(backgroundBrush, backgroundRectangle);
        }
    }
}