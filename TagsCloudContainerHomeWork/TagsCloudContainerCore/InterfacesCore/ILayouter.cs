using System.Drawing;

namespace TagsCloudContainerCore.InterfacesCore;

public interface ILayouter
{
    Rectangle PutNextRectangle(Size size);
}