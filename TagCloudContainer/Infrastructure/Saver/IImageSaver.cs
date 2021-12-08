using System.Drawing;

namespace TagCloud.Infrastructure.Saver;

public interface IImageSaver
{
    void Save(Bitmap bitmap, string outputPath, string outputFormat);
}