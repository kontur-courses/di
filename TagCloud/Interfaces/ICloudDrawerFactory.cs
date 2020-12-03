using System.Drawing;
using TagCloud.Interfaces;

namespace TagCloud
{
    public interface ICloudDrawerFactory
    {
        ICloudDrawer Get(Size pictureSize);
    }
}