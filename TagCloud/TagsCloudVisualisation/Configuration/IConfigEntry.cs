namespace TagsCloudVisualisation.Output
{
    public interface IConfigEntry<T>
    {
        T GetValue(string description);
    }
}