using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Core.ImageBuilder
{
    interface IImageBuilder
    {
        Bitmap Build(string fontName, IEnumerable<Tag> tags, Size size);
    }
}