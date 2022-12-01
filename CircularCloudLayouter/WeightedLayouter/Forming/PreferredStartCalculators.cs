namespace CircularCloudLayouter.WeightedLayouter.Forming;

public static class PreferredStartCalculators
{
    public static readonly Func<int, int, int, int, int> CloserToMiddle =
        (min, max, sideLength, middle) =>
            max < middle + sideLength / 2d
                ? max - sideLength
                : Math.Max(min, (2 * middle - sideLength) / 2);

    public static readonly Func<int, int, int, int, int> CloserToEdges =
        (min, max, sideLength, middle) =>
            middle - min > max - middle ? min : max - sideLength;

    public static readonly Func<int, int, int, int, int> CloserToStart =
        (min, _, _, _) => min;

    public static readonly Func<int, int, int, int, int> CloserToEnd =
        (_, max, sideLength, _) => max - sideLength;
}