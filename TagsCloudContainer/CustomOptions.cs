using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class CustomOptions : ICustomOptions
    {
        public int PictureSize { get; set; }
        public string TextsPath { get; set; } = string.Empty;
        public string WordsFileName { get; set; } = string.Empty;
        public string BoringWordsName { get; set; } = string.Empty;
        public string Font { get; set; } = string.Empty;
        public string BackgroundColor { get; set; } = string.Empty;
        public string FontColor { get; set; } = string.Empty;
        public string ExcludedParticals { get; set; } = string.Empty;
        public int MinTagSize { get; set; }
        public int MaxTagSize { get; set; }
    }
}