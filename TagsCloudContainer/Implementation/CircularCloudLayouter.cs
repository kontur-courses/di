using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public sealed class CircularCloudLayouter : ICircularCloudLayouter
    {
        private Point _center;
        private double _angle = 0.0;
        private double _radius = 1.0;

        /// <summary>
        /// Increment variable for radius of spiral
        /// </summary>
        private const double _INCRADIUS = 0.1;

        /// <summary>
        /// Increment variable for angle of spiral
        /// </summary>
        private const double _ALPHA = 1.0;

        public int Radius => (int)_radius;
        public Point CloudCenterPoint => _center;


        public List<Rectangle> CloudData { get; set; }
        public CircularCloudLayouter(Point center)
        {
            _center = center;
            CloudData = new List<Rectangle>();
        }

        private void IncrementSpiralProps()
        {
            _angle += _ALPHA;
            _radius += _INCRADIUS;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize == default(Size))
                throw new ArgumentNullException();

            return PutNextRectangleImp(rectangleSize);
        }

        private Rectangle PutNextRectangleImp(Size rectangleSize)
        {
            var rectangle = CloudData.Count == 0 ? GetNewRectangle(rectangleSize) : GetNewNonCrossRectangle(rectangleSize);

            CloudData.Add(rectangle);
            return rectangle;
        }

        private Rectangle GetNewRectangle(Size rectangleSize)
        {
            return new Rectangle(GetRectangleLocation(), rectangleSize);
        }

        private Rectangle GetNewNonCrossRectangle(Size rectangleSize)
        {
            Rectangle newRectangle;

            while (true)
            {
                newRectangle = GetNewRectangle(rectangleSize);

                if (NotIntersectWithOutherRectangles(newRectangle))
                    break;

                IncrementSpiralProps();
            }
            return newRectangle;
        }

        private bool NotIntersectWithOutherRectangles(Rectangle newRectangle)
        {
            var intersectCount = CloudData.Count;

            foreach (var rectangle in CloudData)
            {
                if (!newRectangle.IntersectsWith(rectangle))
                    intersectCount--;
            }
            return intersectCount == 0;
        }

        private Point GetRectangleLocation()
        {
            var x = (int)(Math.Sin(_angle) * _radius) + _center.X;
            var y = (int)(Math.Cos(_angle) * _radius) + _center.Y;

            return new Point(x, y);
        }
    }
}
