using System.Drawing;

namespace TagsCloudVisualization;

public interface IPallete
{
    public Color GetNextWordColor();

    public Color GetBackgroundColor();
}