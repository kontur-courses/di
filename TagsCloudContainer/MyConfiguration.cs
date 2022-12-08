namespace TagsCloudContainer
{
    public class MyConfiguration : IMyConfiguration
    {
        public int PictureSize { get; set; }
        public int FontSize { get; set; }
        public string TextsPath { get; set; } = string.Empty;
        public string WordsFileName { get; set; } = string.Empty;
        public string BoringWordsName { get; set; } = string.Empty;
        public string Font { get; set; } = string.Empty;
        public string BackgroundColor { get; set; } = string.Empty;
        public string FontColor { get; set; } = string.Empty;
        public string ExcludedParticals { get; set; } = string.Empty;
    }

    public interface IMyConfiguration
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