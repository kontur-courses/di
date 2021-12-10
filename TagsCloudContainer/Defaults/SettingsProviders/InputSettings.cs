using Mono.Options;
using TagsCloudContainer.Abstractions;

namespace TagsCloudContainer.Defaults.SettingsProviders;

public class InputSettings : IRequiredSettingsProvider
{
    private static string[] paths = Array.Empty<string>();
    private static string source = string.Empty;
    private static bool isSet;

    public string[] Paths { get => paths; private set => paths = value; }
    public string Source { get => source; private set => source = value; }
    public bool IsSet { get => isSet; private set => isSet = value; }

    public OptionSet GetCliOptions()
    {
        var options = new OptionSet()
            {
                {"files=", $"Specifies input files separated by '|'.", v =>
                    {
                        Paths = v.Split('|');
                        IsSet=true;
                    }
                },
                {"string=", $"Specifies string to read.", v =>
                    {
                        Source = v;
                        IsSet=true;
                    }
                }
            };

        return options;
    }
}