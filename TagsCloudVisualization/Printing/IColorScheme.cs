using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IColorScheme
    {
        Color GetColorBy(Size size);
    }
}