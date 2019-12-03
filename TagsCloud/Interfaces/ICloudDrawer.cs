using System.Collections.Generic;
using System.Drawing;
using TagsCloud.WordProcessing;

namespace TagsCloud.Interfaces
{
    interface ICloudDrawer
    {
        Image Paint(IEnumerable<(Tag tag, Rectangle position)> resultTagCloud, Size imageSize, Color backgroundColor, int widthOfBorder = 0);
    }
}
