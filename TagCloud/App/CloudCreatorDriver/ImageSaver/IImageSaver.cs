using System.Drawing;

namespace TagCloud.App.CloudCreatorDriver.ImageSaver;

public interface IImageSaver
{
    bool TrySaveImage(Bitmap image, string fullFileName);
}