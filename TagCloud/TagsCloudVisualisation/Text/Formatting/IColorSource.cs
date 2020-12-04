using System.Drawing;

namespace TagsCloudVisualisation.Text.Formatting
{
    public interface IColorSource
    {
        Color Background { get; }
        Color GetWordColor();
    }
}