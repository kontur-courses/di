using System.Drawing;

namespace TagCloud
{
    public class WhiteBgBlackWordsTheme : ITheme
    {
        public bool IsChecked { get; set; }

        public string Name { get; private set; }

        public WhiteBgBlackWordsTheme()
        {
            IsChecked = true;
            Name = "Black and White theme";
        }

        public Color BackgroundColor => Color.White;

        public Color GetWordFontColor(WordToken wordToken) => Color.Black;
    }
}
