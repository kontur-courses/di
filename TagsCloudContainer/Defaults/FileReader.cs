using Autofac;
using TagsCloudContainer.Defaults.SettingsProviders;
using TagsCloudContainer.Registrations;
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class FileReader : ITextReader
{
    private readonly List<FileInfo> paths = new();

    public FileReader(InputSettings settings) : this(settings.Paths)
    {

    }

    public FileReader(string[] paths)
    {
        foreach (var path in paths)
        {
            this.paths.Add(new(path));
        }
    }

    public IEnumerable<string> ReadLines()
    {
        foreach (var file in paths)
        {
            using var fileStream = file.OpenText();
            var line = fileStream.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                yield return line;
                line = fileStream.ReadLine();
            }
        }
    }

    [Register]
    public static void Register(ContainerBuilder builder)
    {
        builder.RegisterType<FileReader>().AsSelf().As<ITextReader>()
            .UsingConstructor(typeof(InputSettings));
    }
}
