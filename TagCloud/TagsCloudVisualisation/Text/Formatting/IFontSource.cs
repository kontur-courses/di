using System.Drawing;

namespace TagsCloudVisualisation.Text.Formatting
{
    public interface IFontSource
    {
        Font GetFont(string word, int totalWordsCount, int positionInTop);
    }
}