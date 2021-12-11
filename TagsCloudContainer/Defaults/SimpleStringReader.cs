using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Defaults.SettingsProviders;

namespace TagsCloudContainer.Defaults;

public class SimpleStringReader : ITextReader
{
    public SimpleStringReader(InputSettings settings) : this(settings.Source)
    {
    }

    protected SimpleStringReader(string source)
    {
        this.source = source;
    }

    private readonly string source;

    public IEnumerable<string> ReadLines()
    {
        var newLine = Environment.NewLine;
        var previous = 0;
        for (var i = 0; i <= source.Length - newLine.Length; i++)
        {
            i = source.IndexOf(newLine, i);
            if (i == -1)
                break;

            yield return source[previous..i];
            previous = i + newLine.Length;
        }

        yield return source[previous..];
    }
}