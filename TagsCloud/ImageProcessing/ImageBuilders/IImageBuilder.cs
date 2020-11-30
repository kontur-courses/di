using System.Collections.Generic;
using System.Drawing;
using TagsCloud.TagsCloudProcessing;

namespace TagsCloud.ImageProcessing.ImageBuilders
{
    public interface IImageBuilder
    {
        public Bitmap BuildImage(IEnumerable<Tag> tags);
    }
}
