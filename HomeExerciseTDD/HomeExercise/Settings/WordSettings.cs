using System.Collections.Generic;
using System.Drawing;

namespace HomeExerciseTDD.settings
{
    public class WordSettings
    {
        public FontFamily Font { get; }
        public int Coefficient { get; }
        
        public WordSettings(FontFamily font, int coefficient)
        {
            Font = font;
            Coefficient = coefficient;
        }
    }
}