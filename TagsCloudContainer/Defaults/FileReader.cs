using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Defaults.SettingsProviders;

namespace TagsCloudContainer.Defaults;

public class FileReader : ITextReader
{
    private readonly List<FileInfo> paths = new();

    public FileReader(InputSettings settings) : this(settings.Paths)
    {

    }

    protected FileReader(string[] paths)
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
            while (!fileStream.EndOfStream)
            {
                yield return line!;
                line = fileStream.ReadLine();
            }

            yield return line!;
        }
    }
}
