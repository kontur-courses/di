using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2.TextGeometry
{
    public class ColoredSizedWord : IColoredSizedWord
    {
        private Color color;

        private Rectangle size;

        private string word;

        private Font font;

        public ColoredSizedWord(Color color, Rectangle size, string word, Font font)
        {
            this.color = color;
            this.size = size;
            this.word = word;
            this.font = font;
        }

        public Color GetColor()
        {
            return color;
        }

        public Font GetFont()
        {
            return font;
        }

        public Rectangle GetSize()
        {
            return size;
        }

        public string GetWord()
        {
            return word;
        }
    }
}
