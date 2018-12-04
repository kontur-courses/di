using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloud.CloudLayouter
{
    public class Word
    {
        public readonly Rectangle PosRectangle;

        public readonly string Name;

        public Word(Rectangle posRectangle, string name)
        {
            PosRectangle = posRectangle;
            Name = name;
        }
    }
}
