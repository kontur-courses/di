using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface IWordFontPicker
    {
        Dictionary<string, Font> PickFonts(List<(string word, int count)> words);
    }
}
