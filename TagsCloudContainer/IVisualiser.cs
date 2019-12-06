using System.Drawing;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer
{
    public interface IVisualiser
    {
        Bitmap DrawRectangles(ICircularCloudLayouter ccl, (string, Size)[] arr);
    }
}