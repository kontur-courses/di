using System.Drawing;

namespace TagCloud.WordColoring
{
    public interface IWordColoring
    {
        Color GetColor(double factor);
    }
}