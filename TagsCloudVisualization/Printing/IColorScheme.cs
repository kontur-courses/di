using System.Drawing;

namespace TagsCloudVisualization.Printing
{
    public interface IColorScheme
    {
        Color GetColorBy(Size size);
    }
}