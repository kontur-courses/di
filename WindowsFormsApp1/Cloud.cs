using System.Collections.Generic;
using System.Drawing;

namespace WindowsFormsApp1
{
    public class Cloud
    {
        public IEnumerable<Word> Words { get; }

        public Cloud(IEnumerable<Word> words)
        {
            this.Words = words;
        }
    }

    public class Word
    {
        public Word(string value, float fontSize, Rectangle area)
        {
            Value = value;
            FontSize = fontSize;
            Area = area;
        }

        public string Value { get; }
        public float FontSize { get; }
        public Rectangle Area { get; }
    }
}