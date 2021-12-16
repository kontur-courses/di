using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainerCore.InterfacesCore;

public interface IPainter
{
    public Bitmap Paint(
        IEnumerable<TagToRender> tags,
        Size imageSize);
}