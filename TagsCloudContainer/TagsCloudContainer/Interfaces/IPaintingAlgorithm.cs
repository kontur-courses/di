using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface IPaintingAlgorithm
    {
       List<Color> GetColorForTag(IEnumerable<Tag> tags);
    }
}