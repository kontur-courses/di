using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloudVisualization
{
    public class FileImageCreator : ITagCloudImageCreator
    {
        public TagCloudImage CreateTagCloudImage(
            IEnumerable<(Rectangle, string)> tagCloud,
            ImageCreatingOptions options) =>
            throw new NotImplementedException();
    }
}
