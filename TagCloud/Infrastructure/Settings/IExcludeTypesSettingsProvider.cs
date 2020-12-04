using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Settings
{
    public interface IExcludeTypesSettingsProvider
    {
        public WordType[] ExcludedTypes { get; }
    }
}