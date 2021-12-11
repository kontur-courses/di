using System.Drawing;

namespace TagCloud.Visualizers
{
    public interface ITagColoring
    {
        Color GetNextColor();
    }
}
