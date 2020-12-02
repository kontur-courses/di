using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.App
{
    public interface ITagscloudDrawer
    {
        Image GetTagscloud(Dictionary<string, int> words, TagscloudSettings settings, double cloudToImageScaleRatio);
        void SetNewConstellator(IRectanglesConstellator newConstellator);
    }
}
