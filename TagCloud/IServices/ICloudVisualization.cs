using System.Drawing;

namespace TagCloud
{
    public interface ICloudVisualization
    {
        Bitmap GetAndDrawRectangles(int width,int height,string path);
    }
}
