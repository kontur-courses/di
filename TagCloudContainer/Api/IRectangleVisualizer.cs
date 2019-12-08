using System.Drawing;

namespace TagCloudContainer.Api
{
    public interface IRectangleVisualizer
    {
        Image CreateImageWithRectangles();
    }
}