using System.Drawing;

namespace TagsCloudVisualization;

public class CloudLayouter : ICloudLayouter
{
    private readonly IPointGenerator pointGenerator;
    private Dictionary<string, Font> wordsFonts = new();
    private readonly List<Rectangle> createdRectangles = new();

    public CloudLayouter(IPointGenerator pointGenerator, Font font, IEnumerable<string> words)
    {
        this.pointGenerator = pointGenerator;
        
        var wordsCounter = new Dictionary<string, int>();
        
        foreach (var word in words)
            if (!wordsCounter.TryAdd(word, 1)) wordsCounter[word] += 1;
        
        var maxOccurrencies = wordsCounter.Values.Max();
        
        foreach (var pair in wordsCounter)
            wordsFonts.Add(pair.Key, new Font(font.FontFamily, font.Size * pair.Value / maxOccurrencies));
    }
    
    public IEnumerable<TextRectangle> CreateLayout()
    {
        foreach (var wordFont in wordsFonts.OrderByDescending(pair => pair.Value.Size))
        {
            using var smallBitmap = new Bitmap(1, 1);
            using var graphics = Graphics.FromImage(smallBitmap);
            var rectangleSize = graphics.MeasureString(wordFont.Key, wordFont.Value);
            var intSize = new Size((int)rectangleSize.Width, (int)rectangleSize.Height);
            yield return new TextRectangle(wordFont.Key, wordFont.Value, PutNextRectangle(intSize));
        }
    }

    private Rectangle PutNextRectangle(Size rectangleSize)
    {
        while (true)
        {
            var nextPoint = pointGenerator.GetNextPoint();

            var rectangleLocation = new Point(nextPoint.X - rectangleSize.Width / 2,
                nextPoint.Y - rectangleSize.Height / 2);

            var newRectangle = new Rectangle(rectangleLocation, rectangleSize);
            if (createdRectangles.Any(rectangle => rectangle.IntersectsWith(newRectangle)))
                continue;

            createdRectangles.Add(newRectangle);
            return newRectangle;
        }
    }
}