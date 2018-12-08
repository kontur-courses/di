using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class SimpleColorPicker : IWordColorPicker
    {
        public Dictionary<string, Color> PickColors(List<(string word, int count)> words)
        {
            return words.ToDictionary(t => t.word, t => Color.Black);
        }

        public Color BackgroundColor => Color.White;
    }
}
