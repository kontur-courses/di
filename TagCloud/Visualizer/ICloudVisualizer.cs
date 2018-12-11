using System.Collections.Generic;
using System.Drawing;
using TagCloud.Models;

namespace TagCloud.Visualizer
{
    public interface ICloudVisualizer
    {
        Bitmap CreatePictureWithItems(IList<TagItem> items);
    }
}