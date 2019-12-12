using System.Drawing;

namespace TagsCloudContainer.Core.LayoutAlgorithms
{
    interface ILayoutAlgorithm
    {
        Rectangle PutNextRectangle(Size rectangleSize);
        Size GetLayoutSize();
        int GetMaxOffsetFromCenterAlongAxis(Axis axis);
    }
}