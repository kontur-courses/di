using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class CustomOptions : ICustomOptions, ICloneable
    {
        public int PictureSize { get; set; }
        public int MinTagSize { get; set; }
        public int MaxTagSize { get; set; }
        public string WorkingDir { get; set; } = string.Empty;
        public string WordsFileName { get; set; } = string.Empty;
        public string BoringWordsName { get; set; } = string.Empty;
        public string Font { get; set; } = string.Empty;
        public string BackgroundColor { get; set; } = string.Empty;
        public string FontColor { get; set; } = string.Empty;
        public string ExcludedParticals { get; set; } = string.Empty;

        public object Clone()
        {
            return new CustomOptions
            {
                PictureSize = PictureSize,
                WorkingDir = WorkingDir,
                WordsFileName = WordsFileName,
                BoringWordsName = BoringWordsName,
                Font = Font,
                BackgroundColor = BackgroundColor,
                FontColor = FontColor,
                ExcludedParticals = ExcludedParticals,
                MaxTagSize = MaxTagSize,
                MinTagSize = MinTagSize
            };
        }
    }
}