using System.Drawing;

namespace TagCloud.ColoringAlgorithm;

public class SingleColorColoringAlgorithm : IColoringAlgorithm
{
    private readonly Color color;
    
    public SingleColorColoringAlgorithm(Color color)
    {
        this.color = color;
    }
    
    public Color[] GetColors(int count)
    {
        var result = new Color[count];
        Array.Fill(result, color, 0, count);
        return result;
    }
}