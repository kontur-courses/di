namespace TagsCloudContainer.Interfaces
{
    public interface ICustomOptions
    {
        int PictureSize { get; set; }
        int MinTagSize { get; set; }
        int MaxTagSize { get; set; }
        string WorkingDir { get; set; }
        string WordsFileName { get; set; }
        string BoringWordsName { get; set; }
        string Font { get; set; }
        string BackgroundColor { get; set; }
        string FontColor { get; set; }
        string ExcludedParticals { get; set; }
        string ImageFormat { get; set; }
    }
}