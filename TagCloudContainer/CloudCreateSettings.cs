using TagCloudContainer.PointAlgorithm;
using TagCloudContainer.Rectangles;

namespace TagCloudContainer
{
    public class CloudCreateSettings : ICloudCreateSettings
    {
        public CloudCreateSettings(IPointer pointFigure, IRectangleBuilder rectangleBuilder)
        {
            PointFigure = pointFigure;
            RectangleBuilder = rectangleBuilder;
        }
        public IPointer PointFigure { get; }
        public IRectangleBuilder RectangleBuilder { get; set; }
    }
}
