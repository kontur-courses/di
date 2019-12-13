using System.Collections.Generic;
using System.Drawing;
using TagsCloud.TagGenerators;

namespace TagsCloud.Interfaces
{
    public interface ICloudDrawer
    {
        Image Paint(IEnumerable<(Tag tag, Rectangle position)> resultTagCloud, Size imageSize, Color backgroundColor, int widthOfBorder = 0);
    }
}
