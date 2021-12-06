using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Drawing
{
    public interface IDrawer
    {
        Bitmap DrawImage(IEnumerable<Tag> tags);
    }
}