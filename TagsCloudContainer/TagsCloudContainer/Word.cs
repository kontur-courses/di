using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public class Word
    {
        public Rectangle Border { get; }
        public string Text { get; }
        public Font Font{ get; }
        public Word(Rectangle border, string text, Font font)
        {
            Border = border;
            Text = text;
            Font = font;
        }
    }
}
