using System.Drawing;

namespace TagCloud.WordColoring
{
    public interface IWordColoring
    {
        string Name { get; }

        Color GetColor(double factor);
    }
}
