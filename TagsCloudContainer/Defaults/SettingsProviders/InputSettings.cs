using Mono.Options;
using TagsCloudContainer.Abstractions;

namespace TagsCloudContainer.Defaults.SettingsProviders;

public class InputSettings : IRequiredSettingsProvider
{
    public bool UseString { get; private set; }
    public bool UseFile { get; private set; }

    public string[] Paths { get; private set; } = Array.Empty<string>();
    public string Source { get; private set; } = string.Empty;
    public bool IsSet => UseFile || UseString;

    public OptionSet GetCliOptions()
    {
        var options = new OptionSet()
            {
                {"files=", $"Specifies input files separated by '|'.", v =>
                    {
                        Paths = v.Split('|');
                        if(UseString)
                            throw new ArgumentException("Can't use both input providers");
                        UseFile=true;
                    }
                },
                {"string=", $"Specifies string to read.", v =>
                    {
                        Source = v;
                        if(UseFile)
                            throw new ArgumentException("Can't use both file and string input providers");
                        UseString=true;
                    }
                }
            };

        return options;
    }
}