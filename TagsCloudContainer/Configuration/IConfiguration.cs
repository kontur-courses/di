namespace TagsCloudContainer.Configuration
{
    public interface IConfiguration
    {
        string PathToWordsFile { get; }
        string DirectoryToSave { get; }
        string OutFileName { get; }
        string FontFamily { get; }
        string Color { get; }
        int MinFontSize { get; }
        int MaxFontSize { get; }
        int ImageWidth { get; }
        int ImageHeight { get; }
        int RotationAngle { get; }
        int CenterX { get; }
        int CenterY { get; }
        string BoringWordsFileName { get; }
    }
}