using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.TagsClouds;
using TagsCloudContainer.Visualisation;

namespace TagsCloudContainer.Layouting
{
    public class CircularCloudLayouter : ITagsCloudLayouter
    {
        public ITagsCloud TagsCloud { get; }
        private Point Center { get; }
        private readonly List<Rectangle> addedRectangles = new List<Rectangle>();

        private readonly IEnumerator<Point> geometryEnumerator;

        public CircularCloudLayouter(Point center, ITagsCloudFactory tagsCloudFactory)
        {
            Center = center;
            TagsCloud = tagsCloudFactory.CreateTagsCloud();
            const double coefficients = 0.5;
            const double spiralStep = 0.05;
            var geometryObject = new ArchimedeanSpiral(center, coefficients, spiralStep);
            geometryEnumerator = geometryObject.GetEnumerator();
        }

        

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var currentRectangle = new Rectangle
                (Center.X, Center.Y, rectangleSize.Width, rectangleSize.Height);

            while (RectanglesAreIntersecting(currentRectangle))
            {
                var currentPoint = GetNextPoint();
                currentRectangle = new Rectangle
                    (currentPoint.X, currentPoint.Y, rectangleSize.Width, rectangleSize.Height);
            }

            addedRectangles.Add(currentRectangle);
            return currentRectangle;
        }


        private Point GetNextPoint()
        {
            geometryEnumerator.MoveNext();
            return geometryEnumerator.Current;
        }

        private bool RectanglesAreIntersecting(Rectangle rectangle)
        {
            return addedRectangles.Any(rect => rect.IntersectsWith(rectangle));
        }
    }
}