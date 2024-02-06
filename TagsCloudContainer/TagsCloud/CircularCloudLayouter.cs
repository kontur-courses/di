using System.Drawing;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Utility;

namespace TagsCloudContainer.TagsCloud
{
    public class CircularCloudLayouter : ICircularCloudLayouter
    {
        private readonly INextPointProvider pointProvider;

        public Point CloudCenter { get; init; } = Point.Empty;
        public IList<Rectangle> Rectangles { get; init; } = new List<Rectangle>();

        public CircularCloudLayouter(Point center, INextPointProvider pointProvider)
        {
            CloudCenter = center;
            this.pointProvider = pointProvider;
        }

        private const int MinPositiveValue = 1;
        private const int MinWidth = 0;
        private const int MinHeight = 0;

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            ValidateRectangleSize(rectangleSize);

            var currentRectangle = CreateNewRectangle(rectangleSize);
            Rectangles.Add(currentRectangle);

            return currentRectangle;
        }

        public Rectangle PutNextRectangle(string word, Font font)
        {
            var textSize = MeasureTextSize(word, font);
            return PutNextRectangle(textSize);
        }

        private Size MeasureTextSize(string text, Font font)
        {
            // размер минимального временного изображения для измерения текста
            var imageSizeForTextMeasurement = new Size(MinPositiveValue, MinPositiveValue);

            // временное изображение с заданным размером
            using (var temporaryBitmap = new Bitmap(imageSizeForTextMeasurement.Width, imageSizeForTextMeasurement.Height))
            {
                using (var temporaryGraphics = Graphics.FromImage(temporaryBitmap))
                {
                    var textSize = Size.Ceiling(temporaryGraphics.MeasureString(text, font));

                    return textSize;
                }
            }
        }

        private Result<Size> ValidateRectangleSize(Size rectangleSize)
        {
            if (rectangleSize.Width <= MinWidth || rectangleSize.Height <= MinHeight)
            {
                return Result<Size>.Failure("Width and height of the rectangle must be greater than zero");
            }

            return Result<Size>.Success(rectangleSize);
        }

        private Rectangle CreateNewRectangle(Size rectangleSize)
        {
            while (true)
            {
                var nextPoint = pointProvider.GetNextPoint();
                var rectangleLocation = GetUpperLeftCorner(nextPoint, rectangleSize);
                var rectangle = new Rectangle(rectangleLocation, rectangleSize);

                if (!RectanglesIntersect(rectangle))
                {
                    return rectangle;
                }
            }
        }

        private Point GetUpperLeftCorner(Point rectangleCenter, Size rectangleSize)
        {
            var center = TagCloudApp.CalculateCenter(rectangleSize.Width, rectangleSize.Height);
            return new Point(rectangleCenter.X - center.X, rectangleCenter.Y - center.Y);
        }

        private bool RectanglesIntersect(Rectangle newRectangle)
        {
            return Rectangles.Any(rect => rect.IntersectsWith(newRectangle));
        }

    }
}
