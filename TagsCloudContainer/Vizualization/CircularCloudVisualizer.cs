using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace TagsCloudContainer
{
    public class CircularCloudVisualizer : IVisualizer
    {
        private readonly Pen rectangleBorderPen;
        private readonly Brush rectangleFillBrush;
        private readonly Brush backgroundFillBrush;
        private readonly Brush textBrush;

        public CircularCloudVisualizer(
            Brush rectangleFillBrush,
            Brush backgroundFillBrush,
            Pen rectangleBorderPen,
            Brush textBrush)
        {
            this.rectangleFillBrush = rectangleFillBrush;
            this.backgroundFillBrush = backgroundFillBrush;
            this.rectangleBorderPen = rectangleBorderPen;
            this.textBrush = textBrush;
        }

        public void Visualize(IEnumerable<Rectangle> rectangles)
        {
        }

        private Bitmap VisualizeLayout(IEnumerable<Rectangle> rectangles)
        {
            var imageSize = GetImageSize(rectangles);
            if (imageSize.Width == 0 && imageSize.Height == 0)
            {
                return null;
            }

            return GetImage(imageSize, rectangles);
        }

        private static Size GetImageSize(IEnumerable<Rectangle> rectangles)
        {
            var cloudRightBorder = rectangles.Max(rect => rect.Right);
            var cloudBottomBorder = rectangles.Max(rect => rect.Bottom);
            var cloudLeftBorder = rectangles.Min(rect => rect.Left);
            var cloudTopBorder = rectangles.Min(rect => rect.Top);
            return new Size(cloudRightBorder + cloudLeftBorder, cloudBottomBorder + cloudTopBorder);
        }

        private Bitmap GetImage(Size imageSize, IEnumerable<Rectangle> rectangles)
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