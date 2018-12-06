using System.Drawing;

namespace TagsCloudContainer
{
    public struct WordLayout
    {
        public string Word { get; }
        public Rectangle RectangleBorder { get; }
        public Font Font { get; }
        public Color Color { get; }

        public WordLayout(string word, Rectangle rectangleBorder, Font font, Color color)
        {
            Word = word;
            RectangleBorder = rectangleBorder;
            Font = font;
            Color = color;
        }
    }
}
