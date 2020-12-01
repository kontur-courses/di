using System.Drawing;

namespace TagsCloudVisualisation.Text.Formatting
{
    public interface IColorSource
    {
        Brush GetBrush(string word, double distanceFromCenter);
    }
}