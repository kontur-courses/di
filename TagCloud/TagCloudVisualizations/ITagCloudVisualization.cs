using System.Drawing;

namespace TagCloud.TagCloudVisualizations
{
    public interface ITagCloudVisualization
    {
        public void PrepareImage(Graphics graphics, ITagCloudVisualizationSettings settings);
        public void Save(string file, ITagCloudVisualizationSettings settings);
    }
}
