using System.Drawing;

namespace TagCloud.Abstractions;

public interface ICloudCreator
{
    Bitmap CreateTagCloud(IEnumerable<ITag> tags);
}