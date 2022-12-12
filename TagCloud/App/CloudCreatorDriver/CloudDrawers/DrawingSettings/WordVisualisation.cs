using System.Drawing;

namespace TagCloud.App.CloudCreatorDriver.CloudDrawers.DrawingSettings;

public class WordVisualisation : IWordVisualisation
{
    public Color Color { get; }
    public double StartingValue { get; }
    public Font Font { get; }
        
    public WordVisualisation(Color color, double startingValue, Font font)
    {
        Color = color;
        StartingValue = startingValue;
        Font = font;
    }
}