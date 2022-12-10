namespace TagsCloudContainer.Interfaces
{
    public interface ICustomOptions
    {
        int PictureSize { get; set; }
        int FontSize { get; set; }
        string TextsPath { get; set; }
        string WordsFileName { get; set; }
        string BoringWordsName { get; set; }
        string Font { get; set; }
        string BackgroundColor { get; set; }
        string FontColor { get; set; }
        string ExcludedParticals { get; set; }
    }
}