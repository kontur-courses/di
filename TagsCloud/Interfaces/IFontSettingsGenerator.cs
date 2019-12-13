using TagsCloud.FontGenerators;

namespace TagsCloud.Interfaces
{
    public interface IFontSettingsGenerator
    {
        FontSettings GetFontSizeForCurrentWord((string word, int frequency) wordFrequency, int positionByFrequency, int countWords);
    }
}
