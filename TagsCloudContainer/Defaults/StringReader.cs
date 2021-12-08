using Autofac;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Defaults.SettingsProviders;
using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Defaults;

public class StringReader : ITextReader
{
    public StringReader(InputSettings settings) : this(settings.Source)
    {
    }

    public StringReader(string source)
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

            yield return source[previous..];
            previous = i;
        }

        yield return source[previous..];
    }

    [Register]
    public static void Register(ContainerBuilder builder)
    {
        builder.RegisterType<StringReader>().AsSelf().As<ITextReader>()
            .UsingConstructor(typeof(InputSettings));
    }
}