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
            set
            {
                spiral = new ArchimedeanSpiral(value);
                ClearLayout();
            }
        }

        public override void ClearLayout()
        {
            base.ClearLayout();
            spiral = new ArchimedeanSpiral(spiral.Center);
        }

        public override Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle result;
            if (Container.GetFreePoints().Count > 0)
            {
                var pointForRectangle = Container.GetFreePoints().FirstOrDefault(point =>
                    CouldPutRectangle(GetRectangleWithCenterInPoint(point, rectangleSize)));
                if (pointForRectangle != Point.Empty)
                {
                    result = GetRectangleWithCenterInPoint(pointForRectangle, rectangleSize);
                    Container.AddRectangle(result);
                    return result;
                }
            }

            result = GetRectangleInNextPoint(rectangleSize);

            Container.AddRectangle(result);
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
                Container.AddFreePoint(pointForRectangle);
            }
        }
    }
}