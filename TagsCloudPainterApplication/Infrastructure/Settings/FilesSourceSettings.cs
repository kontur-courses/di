namespace TagsCloudPainterApplication.Infrastructure.Settings;

public class FilesSourceSettings
{
    public string BoringTextFilePath { get; set; }

    public FilesSourceSettings()
    {
        BoringTextFilePath = @$"{Environment.CurrentDirectory}..\..\..\..\" + Properties.Settings.Default.boringTextFilePath;
    }

}