using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class SimpleFontPicker : IWordFontPicker
    {
        public Dictionary<string, Font> PickFonts(List<(string word, int count)> words)
        {
            return words.ToDictionary(t => t.word, t => new Font("Arial", 16f));
        }
    }
}
