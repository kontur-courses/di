using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloud.TagsCloudVisualization
{
    public class Tag
    {
        public readonly Rectangle PosRectangle;

        public readonly string Word;

        public readonly int FontSize;

        public readonly int Frequency;

        public Tag(Rectangle posRectangle, string word, int fontSize, int frequency)
        {
            PosRectangle = posRectangle;
            Word = word;
            FontSize = fontSize;
            Frequency = frequency;
        }
    }
}
