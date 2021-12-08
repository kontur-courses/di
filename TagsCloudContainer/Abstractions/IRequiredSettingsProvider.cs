namespace TagsCloudContainer.Abstractions;

public interface IRequiredSettingsProvider : ICliSettingsProvider
{
    bool IsSet { get; }
}
