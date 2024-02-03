using System.Drawing;

namespace TagCloud;

public class ColorGenerator : IColorGenerator
{
    private readonly Color[] colors;
    private int index;

    public ColorGenerator(Color[] colors)
    {
        this.colors = colors;
    }

    public Color GetNextColor()
    {
        index++;
        if (index >= colors.Length)
            index = 0;
        return colors[index];
    }
}