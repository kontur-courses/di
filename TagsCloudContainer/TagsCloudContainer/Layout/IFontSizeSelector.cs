using System.Collections.Generic;

namespace TagsCloudContainer.Layout
{
    public interface IFontSizeSelector
    {
        IEnumerable<FontSizedWord> GetFontSizedWords(IEnumerable<string> words);
    }
}