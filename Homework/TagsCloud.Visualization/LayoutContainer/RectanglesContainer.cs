using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Visualization.ContainerVisitor;
using TagsCloud.Visualization.Extensions;

namespace TagsCloud.Visualization.LayoutContainer
{
    public class RectanglesContainer : ILayoutContainer<Rectangle>
    {
        public IEnumerable<Rectangle> Items { get; init; }

        public (int, int) GetWidthAndHeight()
        {
            var maxRight = Items.Max(x => x.Right);
            var minLeft = Items.Min(x => x.Left);
            var maxBottom = Items.Max(x => x.Bottom);
            var minTop = Items.Min(x => x.Top);

            return (Math.Abs(maxRight - minLeft), Math.Abs(maxBottom - minTop));
        }

        public Point GetCenter() => Items.First().GetCenter();

        public void Accept(Graphics graphics, IContainerVisitor visitor)
        {
            visitor.Visit(graphics, this);
        }
    }
}