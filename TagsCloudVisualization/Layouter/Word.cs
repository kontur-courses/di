using System.Drawing;

namespace TagsCloudVisualization.Layouter
{
    public class Word
    {
        public string Text { get; }
        public Font Font { get; }
        public Rectangle Rectangle { get; }

        public Word(string text, Font font, Rectangle rectangle)
        {
            Text = text;
            Font = font;
            Rectangle = rectangle;
        }
    }
}
