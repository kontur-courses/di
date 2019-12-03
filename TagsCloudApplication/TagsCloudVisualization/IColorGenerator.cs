using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IColorGenerator
    {
        Color GenerateTextColor(string text, Rectangle rectangle);
    }
}
