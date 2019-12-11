using System.Drawing;

namespace TagCloud
{
    public class WhiteBgBlackWordsTheme : ITheme
    {
        public Color BackgroundColor => Color.White;

        public Color GetWordFontColor(WordToken wordToken) => Color.Black;
    }
}
