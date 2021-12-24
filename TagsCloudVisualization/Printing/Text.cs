using System.Drawing;

namespace TagsCloudVisualization.Printing
{
    internal record Text
    {
        public string Word { get; }
        public string Font { get; }
        public Rectangle Rectangle { get; }
        
        public Text(string word, string font, Rectangle rectangle)
        {
            Word = word;
            Font = font;
            Rectangle = rectangle;
        }
        
        public void Deconstruct(out string word, out string font, out Rectangle rectangle)
        {
            (word, font, rectangle) = (Word, Font, Rectangle);
        }
    }
}