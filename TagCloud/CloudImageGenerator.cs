using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public class CloudImageGenerator
    {
        private readonly ICloudLayouter layouter;

        private readonly Color rectangleBorderColor;

        public CloudImageGenerator(ICloudLayouter layouter, Color rectangleBorderColor)
        {
            this.layouter = layouter;
            this.rectangleBorderColor = rectangleBorderColor;
        }

        public Bitmap GenerateBitmap(IEnumerable<Rectangle> layout)
        {
            var layoutArray = layout.ToArray();
            
            var imageSize = GetImageSize(layoutArray);

            var bitmap = new Bitmap(imageSize.Width, imageSize.Height);

            var graphics = Graphics.FromImage(bitmap);

            graphics.TranslateTransform(imageSize.Width / 2f - layouter.CloudCenter.X, imageSize.Height / 2f - layouter.CloudCenter.Y);

            var pen = new Pen(rectangleBorderColor, 1);

            graphics.DrawRectangles(pen, layoutArray.ToArray());

            graphics.Dispose();

            return bitmap;
        }

        public Bitmap GenerateBitmap(IEnumerable<Size> rectanglesSizes)
        {
            var layout = GetLayout(rectanglesSizes);

            return GenerateBitmap(layout);
        }

        private IReadOnlyCollection<Rectangle> GetLayout(IEnumerable<Size> rectanglesSizes)
        {
            return rectanglesSizes.Select(size => layouter.PutNextRectangle(size)).ToList();
        }

        private Size GetImageSize(IEnumerable<Rectangle> layout)
        {
            var minTop = int.MaxValue;
            var maxBottom = -int.MaxValue;
            var minLeft = int.MaxValue;
            var maxRight = -int.MaxValue;

            foreach (var rectangle in layout)
            {
                if (rectangle.Top < minTop)
                    minTop = rectangle.Top;

                if (rectangle.Bottom > maxBottom)
                    maxBottom = rectangle.Bottom;

                if (rectangle.Left < minLeft)
                    minLeft = rectangle.Left;

                if (rectangle.Right > maxRight)
                    maxRight = rectangle.Right;
            }

            var width = 2 * Math.Max(Math.Abs(minLeft), Math.Abs(maxRight)) + 5;

            var height = 2 * Math.Max(Math.Abs(maxBottom), Math.Abs(minTop)) + 5;

            return new Size(width, height);
        }
    }
}
