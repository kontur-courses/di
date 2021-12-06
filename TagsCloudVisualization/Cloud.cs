using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class Cloud : ICloud<ITag>
    {
        private readonly List<ITag> elements;
        public IReadOnlyList<ITag> Elements => elements;
        public PointF Center { get; }

        public Cloud(PointF center)
        {
            Center = center;
            elements = new List<ITag>();
        }

        void ICloud<ITag>.AddTag(ITag tag) 
            => elements.Add(tag);

        public RectangleF GetCloudBoundingRectangle()
        {
            var xMin = 0f;
            var xMax = 0f;
            var yMin = 0f;
            var yMax = 0f;
            foreach (var rect in Elements.Select(tag => tag.Layout))
            {
                
                xMin = Math.Min(xMin, rect.X);
                yMin = Math.Min(yMin, rect.Y);
                xMax = Math.Max(xMax, rect.X + rect.Width);
                yMax = Math.Max(yMax, rect.Y + rect.Height);
            }
            var size = new SizeF(xMax - xMin, yMax - yMin);
            return RectangleFExtensions.GetRectangleByCenter(size, Center);
        }
    }
}
