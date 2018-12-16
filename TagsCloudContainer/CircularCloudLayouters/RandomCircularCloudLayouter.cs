using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.CircularCloudLayouters
{
    public class RandomCircularCloudLayouter:ICircularCloudLayouter
    {
        private readonly List<Rectangle> rectangles = new List<Rectangle>();
        private const int MaxLength = 10000;
        private readonly Point centerPoint;
        private readonly IAngleChooser angleChooser;
        private readonly Rectangle imageRectangle;
        private const int MaxDirectionChoosingIterationsForOneRectangle = 20;

        public RandomCircularCloudLayouter(IImageSettings settings, IAngleChooser angleChooser)
        {
            this.angleChooser = angleChooser;
            var imageSize = settings.ImageSize;
            imageRectangle = new Rectangle(Point.Empty, imageSize);
            centerPoint = new Point(imageSize.Width / 2, imageSize.Height / 2);
        }

        public Rectangle PutNextRectangle(Size size)
        {
            for (var i = 0; i < MaxDirectionChoosingIterationsForOneRectangle; i++)
            {
                var angle = GetNextAngle();
                var rectangle = GetRectangle(size, angle);
                if (!imageRectangle.Contains(rectangle))
                    continue;
                rectangles.Add(rectangle);
                return rectangle;
            }
            throw new InvalidOperationException("Can not add next word: there is no more free space!");
        }

        private double GetNextAngle()
        {
            angleChooser.MoveNext();
            return angleChooser.Current;
        }
        
        

        private Rectangle GetRectangle(Size size, double angle)
        {
            Func<int, Point> leftTopPointFunction =
                x => centerPoint.Sum(new Vector(x, angle)).Shift(-size.Width / 2, -size.Height / 2);
            Func<int, Rectangle> rectangleFunction = x => new Rectangle(leftTopPointFunction(x), size);
            Func<int, bool> intersectionFunction =
                x => !rectangleFunction(x).IntersectsWithPreviousRectangles(rectangles);
            var lengthToCenter = BinarySearch(0, MaxLength, intersectionFunction);
            return rectangleFunction(lengthToCenter.Value);
        }

        private int? BinarySearch(int minValue, int maxValue, Func<int, bool> checkingFunction)
        {
            if (minValue == maxValue)
                return checkingFunction(minValue) ? minValue : new int?();
            var medium = (minValue + maxValue) / 2;
            return checkingFunction(medium) ? BinarySearch(minValue, medium, checkingFunction) : BinarySearch(medium + 1, maxValue, checkingFunction);
        }
                                                                                                                                                                                                                                                                                                                                           
    }
}