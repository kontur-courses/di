using System.Drawing;

namespace TagCloud.ColoringAlgorithm;

public class RandomColoringAlgorithm : IColoringAlgorithm
{
    private readonly Color[] colors;
    private readonly Random rng = new();

    public RandomColoringAlgorithm(Color[] colors)
    {
        this.colors = colors;
    }

    public RandomColoringAlgorithm()
    {
        colors = new[]
        {
            Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.LightBlue, Color.Blue, Color.Purple
        };
    }

    public Color GetNextColor()
    {
        return colors[rng.Next(colors.Length)];
    }
}