using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
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

        public virtual void DrawIn(Graphics graphics, Brush byBrush) => 
            graphics.DrawRectangle(new Pen(byBrush), Frame);

        public override bool Equals(object obj) =>
            obj != null && Frame == (obj as Layout).Frame;
        
    }
}
