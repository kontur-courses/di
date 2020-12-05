using System.Collections.Generic;

namespace TagCloud.Core.Text.Formatting
{
    public interface IFontSizeResolver
    {
        IDictionary<string, float> GetFontSizesForAll(WordWithFrequency[] allWords);
    }
}