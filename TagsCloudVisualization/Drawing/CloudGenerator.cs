using System.Drawing;

namespace TagsCloudVisualization;

public class CloudGenerator
{
    private readonly ILayouter _layouter;
    private readonly Size _maxSize;
    private readonly Size _minSize;
    private readonly Random _random;
    private readonly int _rectanglesCount;

    public CloudGenerator(int count, Size minSize, Size maxSize, ILayouter layouter)
    {
        _random = new Random();
        _rectanglesCount = count;
        _minSize = minSize;
        _maxSize = maxSize;
        _layouter = layouter;
    }

    public List<Rectangle> GetGeneratedCloud()
    {
        var result = new List<Rectangle>();
        for (var i = 0; i < _rectanglesCount; i++)
        {
            var curRectangle = _layouter.PutNextRectangle(GetRandomSize());
            result.Add(curRectangle);
        }

        return result;
    }

    private Size GetRandomSize()
    {
        var width = _random.Next(_minSize.Width, _maxSize.Width);
        var height = _random.Next(_minSize.Height, _maxSize.Height);

        return new Size(width, height);
    }
}