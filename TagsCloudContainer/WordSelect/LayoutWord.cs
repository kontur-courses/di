using System.Drawing;

namespace TagsCloudContainer
{
    public class LayoutWord
    {
        public string Word { get; }
        public Brush Brush { get; }
        public Font Font { get; }
        
        public int Count { get; private set; }

        public LayoutWord(string word, Brush brush, Font font)
        {
            Word = word;
            Brush = brush;
            Font = font;
            Count = 1;
        }
        
        public void Add() => Count++;
    }
}