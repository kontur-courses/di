using System.Drawing;

namespace TagCloud
{
    public interface IVisualizer
    {
        void Visualize(string filename, FontFamily fontFamily, Color stringColor);
    }
}