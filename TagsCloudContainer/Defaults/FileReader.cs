using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class FileReader : ITextReader
{
    private readonly List<FileInfo> paths = new();

    public FileReader(string[] paths)
    {
        foreach (var path in paths)
        {
            this.paths.Add(new(path));
        }
    }

    public IEnumerable<string> ReadLines()
    {
        foreach (var fileStream in paths.Select(x => x.OpenText()))
        {
            var line = fileStream.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                yield return line;
                line = fileStream.ReadLine();
            }

            fileStream.Close();
            fileStream.Dispose();
        }
    }
}
