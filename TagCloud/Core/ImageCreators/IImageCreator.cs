using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Core.ImageCreators
{
    public interface IImageCreator
    {
        public Bitmap Create(IEnumerable<Tag> tags, string fontName, Size size);
    }
}