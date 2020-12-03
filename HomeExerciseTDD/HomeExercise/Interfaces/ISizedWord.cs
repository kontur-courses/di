using System.Drawing;

namespace HomeExerciseTDD
{
    public interface ISizedWord
    {
        public string Text { get; }
        public FontFamily Font { get; }
        public Rectangle Rectangle { get; }
        public int Size { get; }
    }
}