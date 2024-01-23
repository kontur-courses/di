namespace TagsCloudPainterApplication.Infrastructure.Settings
{
    public class FilesSourceSettings
    {
        public string BoringTextFilePath { get; set; } = @$"{Environment.CurrentDirectory}..\..\..\..\Data\BoringWords.txt";
    }
}
