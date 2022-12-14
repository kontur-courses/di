using System.Drawing;

namespace TagsCloudVisualization;

public class CircularLayouterSizeManager : ISizeManager
{
    private readonly ILayouter _layouter;

    public CircularLayouterSizeManager(ILayouter layouter)
    {
        _layouter = layouter;
    }

    public Dictionary<string, Rectangle> GetSizesForWords(Size fieldSize, Dictionary<string, double> freqWords,
        int wordsCount)
    {
        var result = GetWordToRectangle(fieldSize, freqWords, wordsCount);
        var actualFieldSize = GetSize(result.Values.ToList());

        if (actualFieldSize.Width > fieldSize.Width || actualFieldSize.Height > fieldSize.Height)
            throw new Exception(
                $"Не удалось вписать в заданный размер. Получившийся результат: Ширина {actualFieldSize.Width}, Длина {actualFieldSize.Height}");

        return result;
    }

    private Dictionary<string, Rectangle> GetWordToRectangle(Size fieldSize, Dictionary<string, double> freqWords,
        int wordsCount)
    {
        var result = new Dictionary<string, Rectangle>();
        foreach (var key in freqWords.Keys)
        {
            var rectSize = new Size((int)Math.Ceiling(wordsCount * 0.012 * freqWords[key] * fieldSize.Width),
                (int)Math.Ceiling(wordsCount * 0.012 * freqWords[key] * fieldSize.Height));
            var rectangle = _layouter.PutNextRectangle(rectSize);
            result[key] = rectangle;
        }

        return result;
    }

    private Size GetSize(List<Rectangle> rectangles)
    {
        var leftBound = rectangles.Min(rectangle => rectangle.Left);
        var rightBound = rectangles.Max(rectangle => rectangle.Right);
        var bottomBound = rectangles.Max(rectangle => rectangle.Bottom);
        var topBound = rectangles.Min(rectangle => rectangle.Top);

        var width = rightBound - leftBound;
        var height = bottomBound - topBound;

        return new Size(width, height);
    }
}