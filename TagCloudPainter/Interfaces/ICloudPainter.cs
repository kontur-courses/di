using System.Drawing.Imaging;

namespace TagCloudPainter.Interfaces;

public interface ICloudPainter
{
    void PaintTagCloud(string inputPath, string outputPath, ImageFormat format);
}