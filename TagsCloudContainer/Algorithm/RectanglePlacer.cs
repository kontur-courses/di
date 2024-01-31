using System.Diagnostics;
using TagsCloudContainer.Infrastucture;
using TagsCloudContainer.Infrastucture.Settings;

namespace TagsCloudContainer.Algorithm
{
    public sealed class RectanglePlacer : IRectanglePlacer
    {
        private readonly AlgorithmSettings algorithmSettings;
        private readonly ImageSettings imageSettings;

        public RectanglePlacer(AlgorithmSettings algorithmSettings, ImageSettings imageSettings)
        {
            if (imageSettings.Width / 2 < 0 || imageSettings.Height / 2 < 0)
                throw new ArgumentException("the coordinates of the center must be positive numbers");

            this.algorithmSettings = algorithmSettings;
            this.imageSettings = imageSettings;
        }

        public RectangleF GetPossibleNextRectangle(List<TextRectangle> cloudRectangles, SizeF rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("the width and height of the rectangle must be positive numbers");

            return FindPossibleNextRectangle(cloudRectangles, rectangleSize);
        }

        private RectangleF FindPossibleNextRectangle(List<TextRectangle> cloudRectangles, SizeF rectangleSize)
        {
            var radius = algorithmSettings.Radius;
            var angle = algorithmSettings.Angle;
            var center = new Point(imageSettings.Width / 2, imageSettings.Height / 2);

            while (true)
            {
                var point = new Point(
                    (int)(center.X + radius * Math.Cos(angle)),
                    (int)(center.Y + radius * Math.Sin(angle))
                    );
                var possibleRectangle = new RectangleF(point, rectangleSize);

                if (!cloudRectangles.Any(textRectangle => textRectangle.Rectangle.IntersectsWith(possibleRectangle)))
                {
                    return possibleRectangle;
                }

                angle += algorithmSettings.DeltaAngle;
                radius += algorithmSettings.DeltaRadius;
            }
        }

    }
}
