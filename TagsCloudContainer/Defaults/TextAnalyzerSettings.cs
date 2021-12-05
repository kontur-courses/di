using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class TextAnalyzerSettings : ISettingsProvider
{
    public char[] WordSeparators { get; set; } = { ' ' };
}