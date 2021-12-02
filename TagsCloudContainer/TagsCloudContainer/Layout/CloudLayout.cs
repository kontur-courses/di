using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Layout
{
    public class CloudLayout
    {
        public IEnumerable<WordLayout> WordLayouts { get; }
        public Size ImageSize { get; }

        public CloudLayout(IEnumerable<WordLayout> wordLayouts, Size imageSize)
        {
            WordLayouts = wordLayouts;
            ImageSize = imageSize;
        }
    }
}