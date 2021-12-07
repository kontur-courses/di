using Autofac;
using TagsCloudContainer.Defaults.SettingsProviders;
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class StringReader : ITextReader
{
    public StringReader(InputSettings settings)
    {
        source = settings.Source;
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

            yield return source[previous..];
            previous = i;
        }

        yield return source[previous..];
    }

    [Register]
    public static void Register(ContainerBuilder builder)
    {
        builder.RegisterType<StringReader>().AsSelf().As<ITextReader>();
    }
}