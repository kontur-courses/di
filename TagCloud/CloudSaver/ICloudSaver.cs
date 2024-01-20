using System.Drawing;

namespace TagCloud.CloudSaver;

public interface ICloudSaver
{
    void Save(Bitmap bitmap, string OutputPath, string imageFormat);
}