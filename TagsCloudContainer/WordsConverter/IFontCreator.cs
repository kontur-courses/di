namespace TagsCloudContainer.WordsConverter
{
    public interface IFontCreator
    {
        string FontName { get; }
        float GetFontSize(int wordFrequency, int maxWordFrequency);
    }
}