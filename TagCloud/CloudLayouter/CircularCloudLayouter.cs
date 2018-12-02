using System.Drawing;
using TagCloud.PointsSequence;
using TagCloud.RectanglePlacer;

namespace TagCloud.CloudLayouter
{
    public class CircularCloudLayouter : CloudLayouter
    {
        private readonly IPointsSequence pointsSequence;
        private readonly IRectanglePlacer rectanglePlacer;

        /// <summary>
        /// Creates circular cloud layouter.
        /// </summary>
        /// <param name="pointsSequence">Point sequence</param>
        /// <param name="rectanglePlacer">Rectangle placer</param>
        public CircularCloudLayouter(IPointsSequence pointsSequence, IRectanglePlacer rectanglePlacer)
        {
            this.pointsSequence = pointsSequence;
            this.rectanglePlacer = rectanglePlacer;
        }

        /// <summary>
        /// Creates circular cloud layouter with center rectangle placer;
        /// </summary>
        /// <param name="pointsSequence">Point sequence</param>
        public CircularCloudLayouter(IPointsSequence pointsSequence)
        {
            this.pointsSequence = pointsSequence;
            this.rectanglePlacer = new CenterRectanglePlacer();
        }

        protected override Rectangle GetNextRectangle(Size size)
        {
            while (true)
            {
                var point = pointsSequence.GetNextPoint();
                var rectangle = rectanglePlacer.PlaceRectangle(size, point);
                if (IsInsideSurface(rectangle))
                    continue;
                pointsSequence.Reset();
                return rectangle;
            }
        }
    }
}