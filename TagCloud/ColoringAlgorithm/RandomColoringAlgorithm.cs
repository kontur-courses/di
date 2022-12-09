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

    public Color[] GetColors(int count)
    {
        var result = new Color[count];
        
        for (var i = 0; i < count; i++)
        {
            result[i] = colors[rng.Next(colors.Length)];
        }
        return result;
    }
}