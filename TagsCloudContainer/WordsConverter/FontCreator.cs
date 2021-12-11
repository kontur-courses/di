using TagsCloudContainer.Settings;

namespace TagsCloudContainer.WordsConverter
{
    public class FontCreator : IFontCreator
    {
        public string FontName => appSettings.FontName;

        private readonly IAppSettings appSettings;
        private const int MinFontSize = 8;
        private const int MaxFontSize = 28;

        public FontCreator(IAppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public float GetFontSize(int wordFrequency, int maxWordFrequency)
        {
            var size = MaxFontSize * (wordFrequency / (float)maxWordFrequency);
            if (size < MinFontSize)
                return MinFontSize;

            return size;
        }
    }
}