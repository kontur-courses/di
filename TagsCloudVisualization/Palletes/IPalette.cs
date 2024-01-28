using System.Drawing;

namespace TagsCloudVisualization;

public interface IPalette
{
    public Color GetNextWordColor();

    public Color GetBackgroundColor();
}