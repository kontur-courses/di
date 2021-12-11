using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Layouter.PointsProviders;

namespace TagsCloudContainer.Layouter
{
    public class CloudLayouter : ICloudLayouter
    {
        private readonly IPointsProvider pointsProvider;
        private readonly List<Rectangle> rectangles;

        public CloudLayouter(IPointsProvider pointsProvider)
        {
            this.pointsProvider = pointsProvider;
            rectangles = new List<Rectangle>();
        }

        public IReadOnlyCollection<Rectangle> Rectangles => rectangles;


        public Rectangle PutNextRectangle(Size size)
        {
            if (size.Height <= 0 || size.Width <= 0)
                throw new ArgumentException("All size params should be greater than zero!");

            Rectangle nextRectangle;
            do
            {
                nextRectangle = new Rectangle(pointsProvider.GetNextPoint(), size);
            } while (nextRectangle.IntersectsWith(Rectangles));

            rectangles.Add(nextRectangle);
            return nextRectangle;
        }
    }
}