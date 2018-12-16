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
        private readonly Random random = new Random();
        private const int MaxLength = 10000;
        private readonly Point centerPoint;

        public RandomCircularCloudLayouter(IImageSettings settings)
        {
            var imageSize = settings.ImageSize;
            centerPoint = new Point(imageSize.Width / 2, imageSize.Height / 2);
        }

        public Rectangle PutNextRectangle(Size size)
        {
            var angle = GenerateRandomAngle();
            var rectangle = GetRectangle(size, angle);
            rectangles.Add(rectangle);
            Console.WriteLine($"{rectangle.X} {rectangle.Y}");
            return rectangle;
        }

        private Rectangle GetRectangle(Size size, double angle)
        {
            Func<int, Rectangle> rectangleFunction = x => new Rectangle(centerPoint.Sum(new Vector(x, angle)), size);
            Func<int, bool> intersectionFunction =
                x => !rectangleFunction(x).IntersectsWithPreviousRectangles(rectangles);
            var lengthToCenter = BinarySearch(0, MaxLength, intersectionFunction);
            if (lengthToCenter == null)
                throw new InvalidOperationException("Can not add more rectangles");
            return rectangleFunction(lengthToCenter.Value);
        }

        private double GenerateRandomAngle()
        {
            return 2 * Math.PI * random.NextDouble();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           ;
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