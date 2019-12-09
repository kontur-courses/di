using TagsCloud.WordProcessing;

namespace TagsCloud.Interfaces
{
    public interface IFontSettingsGenerator
    {
        FontSettings GetFontSizeForCurrentWord((string word, int frequency) wordFrequency, int positionByFrequency, int countWords);
    }
}
