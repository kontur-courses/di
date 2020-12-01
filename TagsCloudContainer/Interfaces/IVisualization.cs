using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.WordTagsCloud;

namespace TagsCloudContainer.Interfaces
{
    public interface IVisualization
    {
        Bitmap GetBitmapImageCloud(int cloudRadius, List<WordTag> tags);
    }
}