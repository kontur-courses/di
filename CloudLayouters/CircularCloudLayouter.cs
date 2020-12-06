using System.Drawing;
using System.Linq;

namespace CloudLayouters
{
    public class CircularCloudLayouter : BaseCloudLayouter
    {
        private ArchimedeanSpiral spiral;


        public CircularCloudLayouter(Point center)
        {
            Name = "Облако круг";
            spiral = new ArchimedeanSpiral(center);
        }

        public override Point Center
        {
            get => spiral.Center;
            set => ClearLayout(value);
        }

        public override void ClearLayout()
        {
            ClearLayout(Center);
        }

        private void ClearLayout(Point? center = null)
        {
            center ??= spiral.Center;
            base.ClearLayout();
            spiral = new ArchimedeanSpiral((Point) center);
        }

        public override Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle result;
            if (FreePoints.Count > 0)
            {
                var pointForRectangle = FreePoints.FirstOrDefault(point =>
                    CouldPutRectangle(GetRectangleWithCenterInPoint(point, rectangleSize)));
                if (pointForRectangle != Point.Empty)
                {
                    result = GetRectangleWithCenterInPoint(pointForRectangle, rectangleSize);
                    AddRectangle(result);
                    return result;
                }
            }

            result = GetRectangleInNextPoint(rectangleSize);

            AddRectangle(result);
            return result;
        }

        private Rectangle GetRectangleInNextPoint(Size rectangleSize)
        {
            while (true)
            {
                var pointForRectangle = spiral.GetNextPoint();
                var rectangle = GetRectangleWithCenterInPoint(pointForRectangle, rectangleSize);
                if (CouldPutRectangle(rectangle))
                    return rectangle;
                AddFreePoint(pointForRectangle);
            }
        }
    }
}