using System.Collections.Generic;
using System.Drawing;
using TagCloud.Infrastructure.Layout.Environment;
using TagCloud.Infrastructure.Layout.Strategies;

namespace TagCloud.Infrastructure.Layout
{
    // ReSharper disable IdentifierTypo
    public class TagCloudLayouter : ILayouter<SizeF, Point>
    {
        private readonly IEnvironment<RectangleF> environment;
        private readonly ILayoutStrategy<Point> strategy;

        public TagCloudLayouter(IEnvironment<RectangleF> environment, ILayoutStrategy<Point> strategy)
        {
            this.environment = environment;
            this.strategy = strategy;
        }

        public Point Layout(SizeF items)
        {
            throw new System.NotImplementedException();
        }
    }
}