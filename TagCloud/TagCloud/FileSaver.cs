using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    internal class FileSaver : ITagCloudSaver
    {
        public TagCloudImage CreateTagCloudImage(IEnumerable<Rectangle> tagCloud) => throw new NotImplementedException();
    }
}