using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Visualisation;

namespace TagsCloudContainer
{
    public class CircularCloudLayouter : ITagsCloudLayouter
    {
        public ITagsCloud TagsCloud { get; set; }

        private readonly IEnumerator<Point> geometryEnumerator;

        public CircularCloudLayouter(TagsCloudLayouterSettings settings)
        {
            TagsCloud = new TagsCloud(settings.Center);
            const double coefficients = 0.5;
            const double spiralStep = 0.05;
            var geometryObject = new ArchimedeanSpiral(settings.Center, coefficients, spiralStep);
            geometryEnumerator = geometryObject.GetEnumerator();
        }

        public CircularCloudLayouter(Point center)
        {
            TagsCloud = new TagsCloud(center);
            const double coefficients = 0.5;
            const double spiralStep = 0.05;
            var geometryObject = new ArchimedeanSpiral(center, coefficients, spiralStep);
            geometryEnumerator = geometryObject.GetEnumerator();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var currentRectangle = new Rectangle
                (TagsCloud.Center.X, TagsCloud.Center.Y, rectangleSize.Width, rectangleSize.Height);

            while (RectanglesAreIntersecting(currentRectangle))
            {
                var currentPoint = GetNextPoint();
                currentRectangle = new Rectangle
                    (currentPoint.X, currentPoint.Y, rectangleSize.Width, rectangleSize.Height);
            }

            TagsCloud.AddRectangle(currentRectangle);
            return currentRectangle;
        }

       


        private Point GetNextPoint()
        {
            geometryEnumerator.MoveNext();
            return geometryEnumerator.Current;
        }

        private bool RectanglesAreIntersecting(Rectangle rectangle)
        {
            return TagsCloud.AddedRectangles.Any(rect => rect.IntersectsWith(rectangle));
        }
    }
    

   
}