using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IVisualizer
    {
        Bitmap VisualizeTextFromFile(string fileName);
    }
}