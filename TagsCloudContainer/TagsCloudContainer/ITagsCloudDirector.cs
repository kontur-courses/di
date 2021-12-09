using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface ITagsCloudDirector : IDisposable
    {
        Bitmap RenderWords(IEnumerable<string> words);
    }
}