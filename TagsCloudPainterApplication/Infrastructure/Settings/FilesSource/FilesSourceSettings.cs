using TagsCloudPainterApplication.Properties;

namespace TagsCloudPainterApplication.Infrastructure.Settings.FilesSource;

public class FilesSourceSettings : IFilesSourceSettings
{
    private string boringTextFilePath;

    public FilesSourceSettings(IAppSettings settings)
    {
        BoringTextFilePath = settings.BoringTextFilePath;
    }

    public string BoringTextFilePath
    {
        get => boringTextFilePath;
        set => boringTextFilePath = value ?? boringTextFilePath;
    }
}