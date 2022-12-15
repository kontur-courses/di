using System.Drawing;

namespace TagCloud.Common.Saver;

public interface ICloudSaver
{
    public void SaveCloud(Bitmap bmp);
}