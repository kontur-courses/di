using System.Drawing;

namespace TagCloud
{
    public interface IVisualizer
    {
        Bitmap VisualizeTextFromFile(string path);
    }
}
