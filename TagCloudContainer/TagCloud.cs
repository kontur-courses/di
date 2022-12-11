using System.Drawing;
using TagCloudContainer;
using TagCloudContainer.PointAlgorithm;
using TagCloudContainer.Rectangles;
using TagCloudContainer.TagsWithFont;

namespace TagCloudGraphicalUserInterface
{
    public class TagCloud
    {
        private readonly List<TextRectangle> rectangles;
        private readonly List<Point> emptyPoints;

        public TagCloud()
        {
            rectangles = new List<TextRectangle>();
            emptyPoints = new List<Point>();
        }

        public List<TextRectangle> GetRectangles() => rectangles;

        public void CreateTagCloud(ICloudCreateSettings cloudCreator, IEnumerable<ITag> tags)
        {
            var nextSizeRectangle = cloudCreator.RectangleBuilder.GetRectangles(tags).GetEnumerator();
            nextSizeRectangle.MoveNext();
            bool? filledEmptySpaces = false;
            while (true)
                if (TryFillRectangle(cloudCreator.PointFigure, nextSizeRectangle, ref filledEmptySpaces))
                    break;
        }

        private bool TryFillRectangle(IPointer arithmeticSpiral,
            IEnumerator<SizeTextRectangle> nextSizeRectangle, ref bool? nextIteration)
        {
            var point = arithmeticSpiral.GetNextPoint();
            if (nextIteration == null)
                return true;
            nextIteration = FillEmptySpaces(nextIteration, nextSizeRectangle);
            if (!rectangles.Select(x => x.rectangle.Contains(point)).Contains(true))
                emptyPoints.Add(point);
            if (!Contains(rectangles, point, nextSizeRectangle.Current.rectangle))
                nextIteration = AddRectangle(point, nextSizeRectangle);
            return false;
        }

        private bool? FillEmptySpaces(bool? filledEmptySpaced, IEnumerator<SizeTextRectangle> nextSizeRectangle)
        {
            if (filledEmptySpaced.Value && emptyPoints.Any())
            {
                for (var i = 0; i < emptyPoints.Count; i++)
                    if (!Contains(rectangles, emptyPoints[i], nextSizeRectangle.Current.rectangle))
                        AddRectangle(emptyPoints[i], nextSizeRectangle);
                filledEmptySpaced = false;
            }

            return filledEmptySpaced;
        }

        private bool? AddRectangle(Point point, IEnumerator<SizeTextRectangle> nextSizeRectangle)
        {
            var rectangle =
                new Rectangle(
                    point - new Size(nextSizeRectangle.Current.rectangle.Width / 2,
                        nextSizeRectangle.Current.rectangle.Height / 2), nextSizeRectangle.Current.rectangle);
            var textRectangle = new TextRectangle(rectangle, nextSizeRectangle.Current.text,
                nextSizeRectangle.Current.font);
            if (!nextSizeRectangle.MoveNext())
                return null;
            rectangles.Add(textRectangle);
            for (var i = 0; i < emptyPoints.Count; i++)
                if (textRectangle.rectangle.Contains(emptyPoints[i]))
                    emptyPoints.Remove(emptyPoints[i]);
            return true;
        }

        private static bool Contains(IEnumerable<TextRectangle> rectangles, Point point,
            Size size)
        {
            return rectangles
                .Select(x =>
                    x.rectangle.IntersectsWith(new Rectangle(point - new Size(size.Width / 2, size.Height / 2), size)))
                .Contains(true);
        }
    }
}
