using System.Drawing;

namespace TagCloud.TagCloudVisualizations
{
    public interface ITagCloudVisualization
    {
        public void PrepareImage(Graphics graphics,
            ITagCloud tagCloud,
            ITagCloudVisualizationSettings settings);
        public void SaveWithOriginSize(string file,
            ITagCloud tagCloud,
            ITagCloudVisualizationSettings settings);
    }
}
