using System.Drawing;

namespace TagCloud.CloudSaver;

public class CloudSaver : ICloudSaver
{
    public void Save(Bitmap bitmap, string outputPath, string imageFormat)
    {
        using (bitmap)
        {
            bitmap.Save($"{outputPath}.{imageFormat}");
        }
    }
}