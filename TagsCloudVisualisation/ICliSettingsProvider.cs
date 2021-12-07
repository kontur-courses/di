using Mono.Options;

namespace TagsCloudVisualization.Abstractions;

public interface ICliSettingsProvider
{
    OptionSet GetCliOptions();
}