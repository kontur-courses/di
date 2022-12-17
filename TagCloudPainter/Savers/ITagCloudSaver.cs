using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloudPainter.Savers;

public interface ITagCloudSaver
{
    void SaveTagCloud(Bitmap bitmap,string outputPath, ImageFormat format);
}