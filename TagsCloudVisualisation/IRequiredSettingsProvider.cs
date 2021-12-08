namespace TagsCloudVisualization.Abstractions;

public interface IRequiredSettingsProvider : ICliSettingsProvider
{
    bool IsSet { get; }
}
