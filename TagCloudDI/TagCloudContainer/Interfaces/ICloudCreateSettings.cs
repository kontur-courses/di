namespace TagCloudContainer.Interfaces
{
    public interface ICloudCreateSettings
    {
        public IPointer PointFigure { get; }
        public IRectangleBuilder RectangleBuilder { get; set; }
    }
}
