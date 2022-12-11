using System.Drawing;
using TagCloud.App.CloudCreatorDriver.ImageSaver.FileTypes;

namespace TagCloud.App.CloudCreatorDriver.ImageSaver;

public interface IImageSaver
{
    bool TrySaveImage(Bitmap image, IFullFileName fullFileName);
}