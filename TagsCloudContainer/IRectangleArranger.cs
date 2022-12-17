using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface IRectangleArranger
    {
        List<TextContainer> GetContainers(Dictionary<string, int> words, Font baseFont);
    }
}