using System.Drawing;
using TagCloud.Common.Options;

namespace TagCloud.Common.Drawing;

public interface ICloudDrawer
{
    Bitmap DrawCloud(IEnumerable<Tag> tags);
}