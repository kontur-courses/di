using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagCloud.Tag
{
    public class Layout : ITag
    {
        public Rectangle Frame { get; set; }

        public Size Size { get; set; }

        public Layout(Rectangle frame)
        {
            Frame = frame;
        }
        protected Layout(Size size)
        {
            Size = size;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Frame == (obj as Layout).Frame;
        }
    }
}
