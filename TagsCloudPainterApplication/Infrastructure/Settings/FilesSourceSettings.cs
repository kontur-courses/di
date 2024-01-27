namespace TagsCloudPainterApplication.Infrastructure.Settings;

public class FilesSourceSettings
{
    private string boringTextFilePath;

    public FilesSourceSettings()
    {
        BoringTextFilePath = @$"{Environment.CurrentDirectory}..\..\..\..\" +
                             Properties.Settings.Default.boringTextFilePath;
    }

    public string BoringTextFilePath
    {
        get => boringTextFilePath;
        set => boringTextFilePath = value ?? boringTextFilePath;
    }
}