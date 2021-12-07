namespace TagsCloudContainer.Layout
{
    public class FontSizedWord
    {
        public string Word { get; }
        public float FontSize { get; }

        public FontSizedWord(string word, float fontSize)
        {
            Word = word;
            FontSize = fontSize;
        }
    }
}