using System.Drawing;

namespace TagCloud
{
    public interface IVisualizer
    {
        void Visualize(string filename, string fontFamily, Color stringColor);
    }
}