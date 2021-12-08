using Mono.Options;

namespace TagsCloudContainer.Abstractions;

public interface ICliSettingsProvider
{
    OptionSet GetCliOptions();
}