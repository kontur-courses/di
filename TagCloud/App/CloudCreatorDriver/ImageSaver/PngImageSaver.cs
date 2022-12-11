using System.Drawing;
using System.Runtime.InteropServices;

namespace TagCloud.App.CloudCreatorDriver.ImageSaver;

public class PngImageSaver : IImageSaver
{
    public bool TrySaveImage(Bitmap image, FullFileName fullFileName)
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