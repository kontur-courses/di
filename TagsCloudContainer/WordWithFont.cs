using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer
{
    public class WordWithFont
    {
        public string Word { get; set; }
        public Font Font { get; set; }

        public WordWithFont(string word, Font font)
        {
            Word = word;
            Font = font;
        }
    }
}
