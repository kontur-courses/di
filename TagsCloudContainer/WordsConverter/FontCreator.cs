namespace TagsCloudContainer.WordsConverter
{
    public class FontCreator : IFontCreator
    {
        public string FontName { get; }

        private const int MinFontSize = 8;
        private const int MaxFontSize = 32;

        public FontCreator(string fontName)
        {
            FontName = fontName;
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