using System.Drawing;

namespace TagsCloudVisualisation.Text.Formatting
{
    public interface IColorSource
    {
        Color BackgroundColor { get; }
        Brush GetBrush(string word, double distanceFromCenter);
    }
}