using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudGenerator.CloudLayouter
{
    public interface ICloudLayouter
    {
        Cloud LayoutWords(Dictionary<string, int> wordToCount, Font font);
    }
}