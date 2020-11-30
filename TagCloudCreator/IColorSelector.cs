using System.Drawing;

namespace TagCloudCreator
{
    public interface IColorSelector
    {
        string Name { get; }
        Color GetColor(DrawingWord word);
    }
}