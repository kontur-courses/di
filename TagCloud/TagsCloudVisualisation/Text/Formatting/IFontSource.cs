using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualisation.Text.Formatting
{
    public interface IFontSource
    {
        IDictionary<string, Font> GetFontsForAll(WordWithFrequency[] allWords);
    }
}