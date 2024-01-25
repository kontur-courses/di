namespace TagCloud.Domain.Layouter.Interfaces;

public interface ICloudLayouter
{
    public IReadOnlyCollection<Rectangle> Rectangles { get; }
    public Rectangle PutNextRectangle(Size rectangleSize);
}