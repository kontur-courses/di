using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ITextColorProvider
    {
        Color GetTextColor(string text, Rectangle rectangle);
    }
}
