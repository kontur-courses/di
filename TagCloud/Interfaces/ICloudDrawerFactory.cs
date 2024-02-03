using System.Drawing;
namespace TagCloud;

public interface ICloudDrawerFactory
{
    ICloudDrawer Get(Size pictureSize);
}