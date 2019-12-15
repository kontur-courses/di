using System.Drawing;
using TagCloudGenerator.GeneratorCore.CloudLayouters;

namespace TagCloudGenerator.GeneratorCore.TagClouds
{
    public interface ITagCloud
    {
        Bitmap CreateBitmap(string[] cloudStrings, ICloudLayouter cloudLayouter, Size bitmapSize);
    }
}