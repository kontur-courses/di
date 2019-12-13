namespace TagsCloudVisualization.WordSource.Interfaces
{
    public interface ISelector<T>
    {
        bool Select(T obj);
    }
}