using System.Drawing;

namespace TagCloudCreator
{
    public class SimpleWordPainter : IWordPainter
    {
        private readonly Graphics graphics = Graphics.FromImage(new Bitmap(1, 1));

        public void DrawWord(string word, Font font, Brush brush, Graphics graphics, Point location)
        {
            graphics.DrawString(word, font, brush, location);
        }

        public Size GetWordSize(string word, Font font)
        {
            return graphics.MeasureString(word, font).ToSize();
        }
    }
}