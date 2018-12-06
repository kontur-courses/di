namespace TagsCloudContainer.Interfaces
{
    public interface IConfiguration
    {
        string PathToWordsFile { get; }
        string DirectoryToSave { get; }
        string OutFileName { get; }
        string FontFamily { get; }
        string Color { get; }
        int FontSize { get; }
        int ImageWidth { get; }
        int ImageHeight { get; }
        int RotationAngle { get; }
    }
}