using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ITagsCloudLayouter
    {
        public ITagsCloud TagsCloud { get; set; }

        private readonly IEnumerator<Point> geometryEnumerator;

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

        /*public TagsCloudWord PutNextWord(string word, Size wordSize)
        {
            var currentRectangle = new Rectangle
                (TagsCloud.Center.X, TagsCloud.Center.Y, wordSize.Width, wordSize.Height);

            while (RectanglesAreIntersecting(currentRectangle))
            {
                var currentPoint = GetNextPoint();
                currentRectangle = new Rectangle
                    (currentPoint.X, currentPoint.Y, wordSize.Width, wordSize.Height);
            }

            var tagsCloudWord = new TagsCloudWord(word, currentRectangle);
            TagsCloud.AddWord(tagsCloudWord);
            TagsCloud.AddRectangle(currentRectangle);
            return tagsCloudWord;
        }*/

      


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