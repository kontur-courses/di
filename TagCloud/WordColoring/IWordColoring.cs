using System.Drawing;

namespace TagCloud
{
    public interface IWordColoring
    {
        Color GetColor(double factor);
    }
}
