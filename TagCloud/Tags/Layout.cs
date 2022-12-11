using System;
using System.Drawing;
using TagCloud.Extensions;

namespace TagCloud.Tags
{
    public class Layout : ITag
    {
        protected Rectangle frame;
        public Rectangle Frame => frame;
        public Size Size { get; }

        public Layout(Rectangle frame)
        {
            this.frame = frame;
        }

        protected Layout(Size size)
        {
            Size = size;
        }

        public void ShiftTo(Size shift)
        {
            frame = new Rectangle(Frame.Location.ShiftTo(shift), Frame.Size);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj) => 
            Equals(obj as Layout);

        public bool Equals(Layout layout) =>
            layout != null && Frame == layout.Frame;
    }
}
