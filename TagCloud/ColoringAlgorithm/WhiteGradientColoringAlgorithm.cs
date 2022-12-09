using System.Drawing;

namespace TagCloud.ColoringAlgorithm;

public class WhiteGradientColoringAlgorithm : IColoringAlgorithm
{
    private readonly Color color;

    public WhiteGradientColoringAlgorithm()
    {
        this.color = Color.Red;
    }
    
    public WhiteGradientColoringAlgorithm(Color color)
    {
        this.color = color;
    }
    
    public Color[] GetColors(int count)
    {
        var result = new Color[count];

        for (var i = 0; i < count; i++)
        {
            var redCurrent = color.R + (Color.White.R - color.R) * i / (2 * count);
            var greenCurrent = color.G + (Color.White.G - color.G) * i / (2 * count);
            var blueCurrent = color.B + (Color.White.B - color.B) * i / (2 * count);
            
            result[i] = Color.FromArgb(redCurrent, greenCurrent, blueCurrent);
        }

        return result;
    }
}