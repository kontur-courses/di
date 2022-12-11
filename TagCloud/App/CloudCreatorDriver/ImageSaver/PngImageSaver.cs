using System.Drawing;
using System.Runtime.InteropServices;
using TagCloud.App.CloudCreatorDriver.ImageSaver.FileTypes;

namespace TagCloud.App.CloudCreatorDriver.ImageSaver;

public class PngImageSaver : IImageSaver
{
    public bool TrySaveImage(Bitmap image, IFullFileName fullFileName)
    {
        try
        {
            image.Save(fullFileName.Path);
            return true;
        }
        catch
        {
            return false;
        }
    }
}