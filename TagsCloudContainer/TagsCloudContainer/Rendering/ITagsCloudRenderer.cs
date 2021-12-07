using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Rendering
{
    public interface ITagsCloudRenderer : IDisposable
    {
        Bitmap GetBitmap(IEnumerable<WordStyle> words, Size imageSize);
    }
}