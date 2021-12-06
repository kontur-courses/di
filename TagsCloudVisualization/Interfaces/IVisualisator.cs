namespace TagsCloudVisualization.Interfaces
{
    public interface IVisualizator<TElement>
    {
        public void Visualize(IVisualizatorSettings settings, ICloud<TElement> cloud);
    }
}
