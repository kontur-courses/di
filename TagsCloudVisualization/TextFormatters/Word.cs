using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.TextFormatters
{
    public class Word
    {
        public Word(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
        public float Frequency { get; set; } = 0;

        public Font Font;
        public Rectangle Rectangle;
    }
}
