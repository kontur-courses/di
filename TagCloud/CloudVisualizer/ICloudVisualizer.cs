using System.Drawing;
using TagCloud.Models;

namespace TagCloud.CloudVisualizer
{
    public interface ICloudVisualizer
    {
        DrawSettings Settings { get; set; }
        Bitmap CreatePictureWithItems(CloudItem[] cloudItems);
    }
}