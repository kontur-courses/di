using Mono.Options;
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults.SettingsProviders;

public class InputSettings : ICliSettingsProvider
{
    public string[] Paths { get; private set; } = Array.Empty<string>();

    public string Source { get; private set; } = string.Empty;

    public OptionSet GetCliOptions()
    {
        var options = new OptionSet()
            {
                {"files=", $"Specifies input files separated by '|'.", v => Paths = v.Split('|') },
                {"string=", $"Specifies string to read.", v => Source = v }
            };

        return options;
    }
}