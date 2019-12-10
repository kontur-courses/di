using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Logic;

namespace TagsCloudVisualization.Services
{
    public interface IImageGenerator
    {
        Bitmap CreateImage(IEnumerable<Tag> tags, float cloudScale, ImageSettings imageSettings);
    }
}