using System.Drawing;

namespace TagsCloudVisualizationTests.Interfaces
{
    public interface IVisualizer
    {
        public void Draw(Graphics graphics);
        public Size GetBitmapSize();
    }
}