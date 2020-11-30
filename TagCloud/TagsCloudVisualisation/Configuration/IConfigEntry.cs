namespace TagsCloudVisualisation.Configuration
{
    public interface IConfigEntry<T> { T GetValue(string description); }
}