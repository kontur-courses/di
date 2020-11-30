using System.Drawing;

namespace TagsCloudVisualisation.Text.Formatting
{
    public interface IWordFormatter
    {
        Font GetFont(string word, int totalWordsCount, int positionInTop);
        Brush GetBrush(string word, double distanceFromCenter);
    }
}