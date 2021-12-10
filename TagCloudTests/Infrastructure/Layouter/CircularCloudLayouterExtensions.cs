using System;
using System.Drawing;
using System.Linq;
using TagCloud.Infrastructure.Layouter;

namespace TagCloudTests.Infrastructure.Layouter;

internal static class CircularCloudLayouterExtensions
{
    private static readonly Random Random = new(int.MaxValue);

    public static void GenerateRandomLayout(this CircularCloudLayouter layouter, int layoutLength)
    {
        for (var i = 0; i < layoutLength; i++)
            layouter.PutNextRectangle(GetRandomSize());
    }

    public static void GenerateLayoutOfSquares(this CircularCloudLayouter layouter, int layoutLength)
    {
        var size = new Size(10, 10);

        for (var i = 0; i < layoutLength; i++)
            layouter.PutNextRectangle(size);
    }

    public static double CalculateLayoutRadius(this CircularCloudLayouter layouter)
    {
        var layout = layouter.GetLayout();
        var layoutRadius = layout.Max(x => (x.Location + x.Size / 2).GetDistance(layouter.Center));

        return layoutRadius;
    }

    private static Size GetRandomSize()
    {
        return new Size(Random.Next(50, 100), Random.Next(25, 50));
    }
}