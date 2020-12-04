using System.Collections.Generic;

namespace TagsCloudVisualisation.Text.Formatting
{
    public interface IFontSizeResolver
    {
        IDictionary<string, float> GetFontSizesForAll(WordWithFrequency[] allWords);
    }
}