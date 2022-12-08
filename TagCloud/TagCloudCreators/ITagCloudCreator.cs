using System;
using System.Collections.Generic;
using System.Text;

namespace TagCloud.TagCloudCreators
{
    public interface ITagCloudCreator
    {
        TagCloud GenerateTagCloud();
    }
}
