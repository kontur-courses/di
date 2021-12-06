using System;
using System.Drawing;
using System.Linq;
using System.Numerics;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        public ICloud<ITag> Cloud { get; }
        private readonly ISpiral spiral;
        
        public CircularCloudLayouter(ICloud<ITag> cloud, ISpiral spiral)
        {
            Cloud = cloud;
            this.spiral = spiral;
        }

        public void PutNextTag(SizeF tagLayouterSize, ITag tag)
        {
            if (tagLayouterSize.Width <= 0 || tagLayouterSize.Height <= 0)
                throw new ArgumentException("Size should be valid: " +
                    "width and height must be positive");

            var rectangle = RectangleFExtensions
                .GetRectangleByCenter(tagLayouterSize, Cloud.Center);
            var positionedRectangle = FindPositionToRectangle(rectangle);
            tag.ReplaceTagLayout(positionedRectangle);
            Cloud.AddTag(tag);
        }

        private RectangleF FindPositionToRectangle(RectangleF rect)
        {
            while (IsRectangleIntersectedByAnother(rect))
            {
                var rectCenter = spiral.GetNextPointOnSpiral();
                rect = RectangleFExtensions.GetRectangleByCenter(rect.Size, rectCenter);
            }
            return ShiftRectangleToCenter(rect);
        }

        private RectangleF ShiftRectangleToCenter(RectangleF rect)
        {
            var rectCenter = rect.GetCenter();
            var normal = rectCenter.GetNormalToCenter(Cloud.Center);
            if (float.IsNaN(normal.X) || float.IsNaN(normal.Y))
                return rect;
            var shifted = new RectangleF();
            var k = 1;

            while (!IsRectangleIntersectedByAnother(rect))
            {
                shifted = rect;
                rect = GetShiftedRectangle(rect.Size, rectCenter, normal * k);
                k += 1;
            }
            return shifted;
        }

        private RectangleF GetShiftedRectangle
            (SizeF rectSize, PointF center, Vector2 offset)
            => RectangleFExtensions.GetRectangleByCenter(rectSize, new PointF
                (center.X + offset.X, center.Y + offset.Y));

        private bool IsRectangleIntersectedByAnother(RectangleF rect)
            => Cloud.Elements.Any(tag => tag.Layout.IntersectsWith(rect));
    }
}
