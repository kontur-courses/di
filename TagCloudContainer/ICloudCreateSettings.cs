using TagCloudContainer.PointAlgorithm;
using TagCloudContainer.Rectangles;

namespace TagCloudContainer
{
    public interface ICloudCreateSettings
    {
        public IPointer PointFigure { get; }
        public IRectangleBuilder RectangleBuilder { get; set; }
    }
}
