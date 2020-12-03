using System.Drawing;

namespace HomeExerciseTDD
{
    public class Word : IWord
    {
        public string Text { get; }
        public int Frequency { get; }
        public FontFamily Font { get; }
        
        public Word(string text, int frequency, FontFamily font)
        {
            Text = text;
            Frequency = frequency;
            Font = font;
        }
    }
}