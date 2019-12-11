using System.Drawing;

namespace TagCloud
{
    public interface ITheme : ICheckable
    {
        Color BackgroundColor { get; }
        Color GetWordFontColor(WordToken wordToken);
    }
}
