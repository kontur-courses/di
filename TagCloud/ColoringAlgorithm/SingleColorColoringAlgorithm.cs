using System.Drawing;

namespace TagCloud.ColoringAlgorithm;

public class SingleColorColoringAlgorithm : IColoringAlgorithm
{
    private readonly Color color;
    
    public SingleColorColoringAlgorithm(Color color)
    {
        this.color = color;
    }
    
    public Color GetNextColor()
    {
        return color;
    }
}