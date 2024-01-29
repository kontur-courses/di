using System.Drawing;

namespace TagCloudTests;

public class RandomColorSelector : IColorSelector
{
    private readonly Random random;

    public RandomColorSelector()
    {
        random = new Random(DateTime.Now.Microsecond);
    }

    public Color PickColor()
    {
        return Color.FromArgb(255,
            random.Next(0, 255),
            random.Next(0, 255),
            random.Next(0, 255)
        );
    }
}