using TagCloudContainer.Infrastructure.Common;

namespace TagCloudContainer.Infrastructure.FileReader;

public class PlainTextFileReader : IFileReader
{
    private readonly IInputPathProvider settings;

    public PlainTextFileReader(IAppSettings settings)
    {
        this.settings = settings;
    }

    public IEnumerable<string> GetLines()
    {
        if (!File.Exists(settings.InputPath))
            throw new ArgumentException($"The file does not exist {settings.InputPath}");

        return File.ReadLines(settings.InputPath);
    }
}