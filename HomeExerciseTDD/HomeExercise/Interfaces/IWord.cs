using System.Drawing;

namespace HomeExerciseTDD
{
    public interface IWord
    {
        public string Text { get; }
        public int Frequency { get; }
        public FontFamily Font { get; }
    }
}