using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Rendering
{
    public interface ITagsCloudImageSaver : IDisposable
    {
        void Save(IEnumerable<WordStyle> words, Size imageSize);
    }
}