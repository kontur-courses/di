using System.Collections.Generic;
using System.Drawing;

namespace WordCloudImageGenerator
{
    public interface ITagCloudVizualizer
    {
        Bitmap DrawItems(List<CloudItem> items);
    }
}