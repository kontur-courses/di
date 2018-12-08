using System;
using System.Collections.Generic;
using System.Drawing;
using Extensions;

namespace TagCloud.Layouters
{
    public class RowLayout
    {
        private Point center;
        private readonly List<Rectangle> body;
        private bool isLeftRectange;
        
        public RowLayout(Rectangle initial)
        {
            Bounds = initial;
            body = new List<Rectangle>{initial};
            center = initial.Center();
        }
        
        public Rectangle Bounds { get; private set; }
        public IEnumerable<Rectangle> Body => body;

        public Rectangle Add(Size rectangleSize)
        {
            if (rectangleSize.Height > Bounds.Height)
                throw new ArgumentException("Size does not fit into row.");


            var rectLoc = isLeftRectange
                ? new Point(Bounds.Left - rectangleSize.Width, Bounds.Top)
                : new Point(Bounds.Right, Bounds.Top);

            var rect = new Rectangle(rectLoc, rectangleSize);
            body.Add(rect);
            Bounds = new []{rect,Bounds}.EnclosingRectangle();
            isLeftRectange = !isLeftRectange;
            return rect;
        }
    }
}