using System.Drawing;

namespace TagCloud.ColoringAlgorithm;

public class GradientColoringAlgorithm : IColoringAlgorithm
{
    private readonly Color from;
    private readonly Color to;

    public GradientColoringAlgorithm(Color[] colors)
    {
        from = colors[0];
        to = colors[1];
    }
    
    public Color[] GetColors(int count)
    {
        var result = new Color[count];

        for (var i = 0; i < count; i++)
        {
            var redCurrent = from.R + (to.R - from.R) * i / (2 * count);
            var greenCurrent = from.G + (to.G - from.G) * i / (2 * count);
            var blueCurrent = from.B + (to.B - from.B) * i / (2 * count);
            
            result[i] = Color.FromArgb(redCurrent, greenCurrent, blueCurrent);
        }

        return result;
    }
}