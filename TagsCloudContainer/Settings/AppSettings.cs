namespace TagsCloudContainer.Settings;

public class AppSettings: IAppSettings
{
    public string InputFile { get; set; } = @"C:\Users\mukan\Documents\Учёба\ShPORA\di\TagsCloudContainer\Data\Input.txt";

    public string OutputFile { get; set; } = @"C:\Users\mukan\Documents\Учёба\ShPORA\di\TagsCloudContainer\Data\Output.gif";
}