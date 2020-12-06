using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IFontSizeCalculator
    {
        IEnumerable<WordWithFont> CalculateFontSize(IEnumerable<string> words, string fontFamily);
    }
}