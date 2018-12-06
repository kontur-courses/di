namespace TagsCloudVisualization
{
    public interface IProvider<out T>
    {
        T GetNext();
    }
}
