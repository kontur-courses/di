using System.Drawing;

namespace TagsCloudVisualization.UI
{
    public interface IVisualizer
    {
        void Start(string[] args);
        Bitmap GetTagCloud();
    }
}