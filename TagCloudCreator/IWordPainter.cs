using System.Drawing;

namespace TagCloudCreator
{
    public interface IWordPainter
    {
        void DrawWord(string word, Font font, Brush brush, Graphics graphics, Point location);
        Size GetWordSize(string word, Font font);
    }
}