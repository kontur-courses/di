using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    internal class FileSaver : ITagCloudSaver
    {
        public TagCloudImage
            CreateTagCloudImage(IEnumerable<(Rectangle, WordInfo)> tagCloud, TagCloudOptions options) =>
            throw new NotImplementedException();
    }
}
