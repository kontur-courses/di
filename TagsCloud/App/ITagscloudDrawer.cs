using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public interface ITagsCloudDrawer
    {
        Image GetTagsCloud(Dictionary<string, int> words, TagsCloudSettings settings, double cloudToImageScaleRatio);
        void SetNewLayouter(IRectanglesLayouter newConstellator);
    }
}
