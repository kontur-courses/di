namespace TagCloud.Infrastructure.Settings
{
    public interface IExcludeTypesSettingsProvider
    {
        public string[] ExcludedTypes { get; }
    }
}