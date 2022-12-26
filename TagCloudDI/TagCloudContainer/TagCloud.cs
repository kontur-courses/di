using System.Drawing;
using TagCloudContainer.Interfaces;
using TagCloudContainer.Models;

namespace TagCloudContainer
{
    public class TagCloud : ITagCloud
    {
        private readonly List<RectangleWithText> rectangles;
        private readonly List<Point> emptyPoints;

        public TagCloud()
        {
            rectangles = new List<RectangleWithText>();
            emptyPoints = new List<Point>();
        }

        public List<RectangleWithText> GetRectangles() => rectangles;

        public void CreateTagCloud(
            IPointProvider pointFigure,
            IRectangleBuilder rectangleBuilder,
            IEnumerable<ITag> tags)
        {
            var nextSizeRectangle = rectangleBuilder.GetRectangles(tags).GetEnumerator();
            nextSizeRectangle.MoveNext();
            bool? filledEmptySpaces = false;

            while (!TryFillRectangle(pointFigure, nextSizeRectangle, ref filledEmptySpaces));
        }

        private bool TryFillRectangle(IPointProvider arithmeticSpiral,
            IEnumerator<RectangleWithText> nextSizeRectangle, ref bool? nextIteration)
        {
            var point = arithmeticSpiral.GetNextPoint();

            if (nextIteration == null)
                return true;

            nextIteration = FillEmptySpaces(nextIteration, nextSizeRectangle);

            if (!rectangles.Select(x => x.Rectangle.Contains(point)).Contains(true))
                emptyPoints.Add(point);

            if (!HasIntersection(rectangles, point, nextSizeRectangle.Current.Rectangle.Size))
                nextIteration = AddRectangle(point, nextSizeRectangle);

            return false;
        }

        private bool? FillEmptySpaces(bool? filledEmptySpaced, IEnumerator<RectangleWithText> nextRectangle)
        {
            if (filledEmptySpaced.Value && emptyPoints.Any())
            {
                for (var i = 0; i < emptyPoints.Count; i++)
                    if (!HasIntersection(rectangles, emptyPoints[i], nextRectangle.Current.Rectangle.Size))
                        AddRectangle(emptyPoints[i], nextRectangle);
                filledEmptySpaced = false;
            }

            return filledEmptySpaced;
        }

        private bool? AddRectangle(Point point, IEnumerator<RectangleWithText> nextSizeRectangle)
        {
            var rectangle =
                new Rectangle(
                    point - new Size(nextSizeRectangle.Current.Rectangle.Width / 2,
                        nextSizeRectangle.Current.Rectangle.Height / 2), nextSizeRectangle.Current.Rectangle.Size);

            var textRectangle = new RectangleWithText(rectangle, nextSizeRectangle.Current.Text,
                nextSizeRectangle.Current.Font);

            if (!nextSizeRectangle.MoveNext())
                return null;

            rectangles.Add(textRectangle);
            for (var i = 0; i < emptyPoints.Count; i++)
                if (textRectangle.Rectangle.Contains(emptyPoints[i]))
                    emptyPoints.Remove(emptyPoints[i]);
            return true;
        }

        private static bool HasIntersection(IEnumerable<RectangleWithText> rectangles, Point point,
            Size size)
        {
            return rectangles
                .Any(x => x.Rectangle
                    .IntersectsWith(new Rectangle(point - new Size(size.Width / 2, size.Height / 2), size)));
        }
    }
}
