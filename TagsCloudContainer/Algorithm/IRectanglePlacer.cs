using TagsCloudContainer.Infrastucture;

namespace TagsCloudContainer.Algorithm
{
    public interface IRectanglePlacer
    {
        RectangleF GetPossibleNextRectangle(List<TextRectangle> cloudRectangles, SizeF rectangleSize);
    }
}