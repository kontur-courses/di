using System.Drawing;

namespace TagCloud.TextColoration
{
    public interface ITextColoration
    {
        Brush GetTextColor(string word, int frequency);
    }
}