using System.Drawing;

namespace TagCloudPainter.Coloring;

public class RainbowWordColoring : IWordColoring
{
    private readonly Color[] colors =
    {
        Color.Red,
        Color.Orange,
        Color.Yellow,
        Color.Green,
        Color.CornflowerBlue,
        Color.Blue,
        Color.Purple
    };

    private int indexColor = -1;

    public Color GetColor()
    {
        UpdateIndexColor();
        return colors[indexColor];
    }

    private void UpdateIndexColor()
    {
        indexColor = indexColor == 6 ? 0 : ++indexColor;
    }
}