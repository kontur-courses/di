using System.Drawing;

namespace TagsCloudVisualization.ColorGenerator;

public class RainbowColorGenerator : IColorGenerator
{
    private readonly Random random;

    private static readonly IReadOnlyList<Color> Colors = new List<Color>
    {
        Color.Red,
        Color.Orange,
        Color.Yellow,
        Color.Green,
        Color.LightBlue,
        Color.Blue,
        Color.Purple
    };

    public RainbowColorGenerator(Random random)
    {
        this.random = random;
    }
    
    public Color Generate()
    {
        return Colors[random.Next(Colors.Count)];
    }
}