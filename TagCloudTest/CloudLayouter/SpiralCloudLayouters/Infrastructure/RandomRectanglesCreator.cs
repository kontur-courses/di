using System.Drawing;
using TagCloud.App.CloudCreatorDriver.RectanglesLayouters.SpiralCloudLayouters;

namespace TagCloudTest.CloudLayouter.SpiralCloudLayouters.Infrastructure;

public static class RandomRectanglesCreator
{
    private static readonly Random Random = new Random();
        
    public static Rectangle[] GetRectangles(int count, Point center, double spiralStep = 1d)
    {
        var circularCloudLayouter = new SpiralCloudLayouter();
        var settings = new SpiralCloudLayouterSettings(center, spiralStep, 0.1);
        circularCloudLayouter.SetSettings(settings);
        var rectangles = GetSizesOfRectangle(count)
            .OrderByDescending(size => size.Height)
            .ThenByDescending(size => size.Width)
            .Select(size => circularCloudLayouter.PutNextRectangle(size))
            .ToArray();
        return rectangles;
    }

    private static IEnumerable<Size> GetSizesOfRectangle(int count,
        int minWidth = 10, int maxWidth = 100, int minHeight = 5, int maxHeight = 40)
    {
        for (var i = 0; i < count; i++)
        {
            var width = Random.Next(minWidth, maxWidth);
            var height = Random.Next(minHeight, maxHeight);
            if (width < height)
            {
                i--;
                continue;
            }
            yield return new Size(width, height);
        }
    }
}