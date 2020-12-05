using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IFontSizeCalculator
    {
        public IEnumerable<WordWithFont> CalculateFontSize(IEnumerable<string> words, string fontFamily);
    }
}