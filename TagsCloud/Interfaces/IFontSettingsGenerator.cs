using System.Drawing;

namespace TagsCloud.Interfaces
{
    public interface IFontSettingsGenerator
    {
        Font GetFontSizeForCurrentWord((string word, int frequency) wordFrequency, int positionByFrequency, int countWords);
    }
}
