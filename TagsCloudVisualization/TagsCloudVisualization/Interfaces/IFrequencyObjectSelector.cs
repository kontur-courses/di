namespace TagsCloudVisualization.Interfaces
{
    internal interface IFrequencyObjectSelector<T>
    {
        bool Select(T obj);
    }
}