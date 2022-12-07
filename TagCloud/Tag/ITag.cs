using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagCloud.Tag
{
    public interface ITag
    {
        public Rectangle Frame { get; set; }
        public Size Size { get; set; }
    }
}
