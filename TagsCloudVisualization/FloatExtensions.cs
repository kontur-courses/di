namespace TagsCloudVisualization;

public static class FloatExtensions
{
    public static bool IsEqualTo(this float number, float another, float accuracy = Layout.Accuracy)
    {
        return Math.Abs(number - another) < accuracy;
    }
}