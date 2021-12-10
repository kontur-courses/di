using Mono.Options;
using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Abstractions;

public interface ICliSettingsProvider : ISingletonService
{
    OptionSet GetCliOptions();
}