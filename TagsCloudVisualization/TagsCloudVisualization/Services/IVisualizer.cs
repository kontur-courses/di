using System.Drawing;

namespace TagsCloudVisualization.Services
{
    public interface IVisualizer
    {
        Bitmap VisualizeTextFromFile(string fileName);
    }
}