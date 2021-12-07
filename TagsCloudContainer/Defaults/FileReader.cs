using Autofac;
using TagsCloudContainer.Defaults.SettingsProviders;
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class FileReader : ITextReader
{
    private readonly List<FileInfo> paths = new();

    public FileReader(InputSettings settings)
    {
        foreach (var path in settings.Paths)
        {
            paths.Add(new(path));
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
        builder.RegisterType<FileReader>().AsSelf().As<ITextReader>();
    }
}
