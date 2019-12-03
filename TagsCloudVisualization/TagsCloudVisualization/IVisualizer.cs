using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IVisualizer
    {
        TagCloud VisualizeTextFromFile(string fileName);
    }
}