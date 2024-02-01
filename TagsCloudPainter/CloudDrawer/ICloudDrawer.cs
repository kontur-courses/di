using System.Drawing;

namespace TagsCloudPainter.Drawer;

public interface ICloudDrawer
{
    public Bitmap DrawCloud(TagsCloud cloud, int imageWidth, int imageHeight);
}