namespace TagsCloudVisualization.Visualizer
{
    public interface IVisualizer<out T>
    {
        T Draw();
    }
}