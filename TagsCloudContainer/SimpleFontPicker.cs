using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class SimpleFontPicker : IWordFontPicker
    {
        private float size = 16;

        public void SetBaseSize(float size)
        {
            this.size = size;
        }

        public Dictionary<string, Font> PickFonts(List<(string word, int count)> words)
        {
            return words.ToDictionary(t => t.word, t => new Font("Arial", size));
        }
    }
}
