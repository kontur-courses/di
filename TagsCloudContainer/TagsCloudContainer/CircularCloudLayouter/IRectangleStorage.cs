using System.Drawing;


namespace TagsCloudContainer.CircularCloudLayouter
{
    public interface IRectangleStorage
    {
        Rectangle PlaceNewRectangle(Size rectangleSize);
    }
}