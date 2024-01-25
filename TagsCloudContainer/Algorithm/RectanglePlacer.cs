using TagsCloudContainer.Infrastucture;
using TagsCloudContainer.Infrastucture.Settings;

namespace TagsCloudContainer.Algorithm
{
    public sealed class RectanglePlacer: IRectanglePlacer
    {
        private Point center;
        private double radius;
        private double angle;
        private double deltaRadius;
        private double deltaAngle;

        public RectanglePlacer(AlgorithmSettings algorithmSettings, Point center)
        {
            if (center.X < 0 || center.Y < 0)
                throw new ArgumentException("the coordinates of the center must be positive numbers");
            this.center = center;
            this.deltaRadius = algorithmSettings.DeltaRadius;
            this.deltaAngle = algorithmSettings.DeltaAngle;
            this.radius = algorithmSettings.Radius;
            this.angle = algorithmSettings.Angle;
        }

        public RectangleF GetPossibleNextRectangle(List<TextRectangle> cloudRectangles, SizeF rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("the width and height of the rectangle must be positive numbers");

            return FindPossibleNextRectangle(cloudRectangles, rectangleSize);
        }

        private RectangleF FindPossibleNextRectangle(List<TextRectangle> cloudRectangles, SizeF rectangleSize)
        {
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

                angle += deltaAngle;
                radius += deltaRadius;
            }
        }

    }
}
