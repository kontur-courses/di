using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class SimpleColorPicker : IWordColorPicker
    {
        public Color BackgroundColor { get; private set; } = Color.White;
        // ReSharper disable once MemberCanBePrivate.Global
        public Color TextColor { get; private set; } = Color.Black;

        public Dictionary<string, Color> PickColors(List<(string word, int count)> words)
        {
            return words.ToDictionary(t => t.word, t => TextColor);
        }

        public void SetBaseWordColor(Color color)
        {
            TextColor = color;
        }

        public void SetBackgroundColor(Color color)
        {
            BackgroundColor = color;
        }
    }
}
