using System.Collections.Generic;
using System.Drawing;


namespace TagsCloudContainer
{
    public interface IRectangleLayout
    {
        Rectangle PutNextRectangle(Size size);
        IEnumerable<Rectangle> GetCoordinatesToDraw();
    }
}
