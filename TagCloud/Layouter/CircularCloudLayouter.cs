using System.Drawing;
using TagCloud.PointsSequence;
using TagCloud.RectanglePlacer;

namespace TagCloud.Layouter
{
    public class CircularCloudLayouter : CloudLayouter
    {
        private readonly IPointsSequence pointsSequence;
        private readonly IRectanglePlacer rectanglePlacer;
       

        public CircularCloudLayouter(IPointsSequence pointsSequence, IRectanglePlacer rectanglePlacer)
        {
            this.pointsSequence = pointsSequence;
            this.rectanglePlacer = rectanglePlacer;
        }

        public CircularCloudLayouter(IPointsSequence pointsSequence)
        {
            this.pointsSequence = pointsSequence;
            rectanglePlacer = new CenterRectanglePlacer();
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