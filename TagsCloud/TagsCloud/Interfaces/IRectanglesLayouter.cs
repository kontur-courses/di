namespace TagsCloudGenerator.Interfaces
{
    public interface IRectanglesLayouter : IResettable
    {
        System.Drawing.RectangleF PutNextRectangle(System.Drawing.SizeF rectangleSize);
    }
}