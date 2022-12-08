namespace TagCloud.TagCloudVisualizations
{
    public interface ITagCloudVisualization
    {
        public void Visualize(ITagCloudVisualizationSettings settings);
        public void Save(string file, ITagCloudVisualizationSettings settings);
    }
}
