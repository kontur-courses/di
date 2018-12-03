using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization;
using Point = System.Drawing.Point;

namespace TagCloud
{
    internal class FileSaver : ITagCloudSaver
    {
        public TagCloudImage CreateTagCloudImage(IEnumerable<(Rectangle, WordInfo)> tagCloud)
        {
            throw new NotImplementedException();
        }
    }
}