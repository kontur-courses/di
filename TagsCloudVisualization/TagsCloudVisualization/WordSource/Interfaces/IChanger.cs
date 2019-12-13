namespace TagsCloudVisualization.WordSource.Interfaces
{
    public interface IChanger<T>
    {
        T Change(T word);
    }
}