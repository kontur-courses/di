using System.Drawing;
using TagCloudContainer.Filters;
using TagCloudContainer.Formatters;
using TagCloudContainer.FrequencyWords;
using TagCloudContainer.Parsers;
using TagCloudContainer.PointAlgorithm;
using TagCloudContainer.Readers;
using TagCloudContainer.Rectangles;
using TagCloudContainer.TagsWithFont;

namespace TagsCloudVisualization
{
    public class TagCloud
    {
        private readonly List<TextRectangle> rectangles;
        private readonly List<Point> emptyPoints;
        private readonly IEnumerable<FontTag> tags;
        private Size srcSize;

        public TagCloud(IEnumerable<FontTag> tags)
        {
            this.tags = tags;
            rectangles = new List<TextRectangle>();
            emptyPoints = new List<Point>();
        }

        public List<TextRectangle> GetRectangles() => rectangles;
        public Size GetScreenSize() => srcSize;

        public void CreateTagCloud(IRectangleBuilder circularCloudLayouter, IPointer arithmeticSpiral)
        {

            var nextSizeRectangle = circularCloudLayouter.GetNextRectangle(tags).GetEnumerator();
            nextSizeRectangle.MoveNext();
            bool? filledEmptySpaces = false;
            while (true)
                if (TryFillRectangle(arithmeticSpiral, nextSizeRectangle, ref filledEmptySpaces))
                    break;
            srcSize = new Size((int)(emptyPoints.Max(x => x.X) * 2.5),
                (int)(emptyPoints.Max(x => x.Y) * 2.5));
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

        private static bool Contains(List<TextRectangle> rectangles, Point point,
            Size size)
        {
            return rectangles
                .Select(x =>
                    x.rectangle.IntersectsWith(new Rectangle(point - new Size(size.Width / 2, size.Height / 2), size)))
                .Contains(true);
        }
        public static TagCloud InitialCloud(string pathTxtFile, FontFamily font, int maxFont, int minFont)
        {
            var fileReader = new TxtReader().Read(pathTxtFile);
            var parser = new FileLinesParser();
            var parsedText = parser.Parse(fileReader);
            var filterWords = new FilterWords();
            var filtredTags = filterWords.Filter(parsedText);
            var formatter = new WordFormatter();
            var formattedTags = formatter.Normalize(filtredTags, x => x.ToLower());
            var freqtag = new FrequencyTags();
            var freqTags = freqtag.GetWordsFrequency(formattedTags);
            var fontSizer = new FontSizer();
            var fontTags = fontSizer.GetTagsWithSize(freqTags, font, maxFont, minFont);
            return new TagCloud(fontTags);
        }
    }
}
