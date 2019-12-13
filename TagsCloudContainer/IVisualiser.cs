using System.Drawing;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer
{
    public interface IVisualiser
    {
        Bitmap DrawRectangles(ICloudLayouter ccl, (string, Size)[] arr);
    }
}