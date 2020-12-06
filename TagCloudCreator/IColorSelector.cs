using System.Drawing;

namespace TagCloudCreator
{
    public interface IColorSelector
    {
        Color GetColor(DrawingWord word);
    }
}