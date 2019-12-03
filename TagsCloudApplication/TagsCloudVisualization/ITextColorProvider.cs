using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ITextColorProvider
    {
        Color GenerateTextColor(string text, Rectangle rectangle);
    }
}
