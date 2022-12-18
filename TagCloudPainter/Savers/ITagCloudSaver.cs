using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloudPainter.Savers;

public interface ITagCloudSaver
{
    void SaveTagCloud(string inputPath,string outputPath, ImageFormat format);
}