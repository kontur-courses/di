using System.Collections.Generic;

namespace TagsCloudContainer.Layout
{
    public interface IFontSizeSelector
    {
        IEnumerable<FontSizedWord> GetFontSizes(IEnumerable<string> words);
    }
}