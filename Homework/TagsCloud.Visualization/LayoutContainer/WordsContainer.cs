using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Visualization.ContainerVisitor;
using TagsCloud.Visualization.Extensions;
using TagsCloud.Visualization.Models;

namespace TagsCloud.Visualization.LayoutContainer
{
    public class WordsContainer : ILayoutContainer<WordWithBorder>
    {
        public IEnumerable<WordWithBorder> Items { get; init; }

        public (int, int) GetWidthAndHeight()
        {
            var rectangles = Items.Select(x => x.Border).ToArray();
            var maxRight = rectangles.Max(x => x.Right);
            var minLeft = rectangles.Min(x => x.Left);
            var maxBottom = rectangles.Max(x => x.Bottom);
            var minTop = rectangles.Min(x => x.Top);

            return (Math.Abs(maxRight - minLeft), Math.Abs(maxBottom - minTop));
        }

        public Point GetCenter() => Items.First().Border.GetCenter();

        public void Accept(Graphics graphics, IContainerVisitor visitor)
        {
            visitor.Visit(graphics, this);
        }
    }
}