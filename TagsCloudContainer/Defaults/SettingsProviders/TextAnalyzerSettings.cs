using Mono.Options;
using TagsCloudContainer.Abstractions;

namespace TagsCloudContainer.Defaults.SettingsProviders;

public class TextAnalyzerSettings : ICliSettingsProvider
{
    public char[] WordSeparators { get => wordSeparators; private set => wordSeparators = value; }
    private const char separator = '/';
    private static char[] wordSeparators = { ' ', ',', '.' };

    public OptionSet GetCliOptions()
    {
        var options = new OptionSet()
        {
            {
                "word-separators=",
                $"Set separators to separate words, separated by '{separator}'. Defaults to '{string.Join(separator, WordSeparators)}'",
                v => WordSeparators = v.Split(separator).Cast<char>().ToArray()
            }
        };

        return options;
    }
}