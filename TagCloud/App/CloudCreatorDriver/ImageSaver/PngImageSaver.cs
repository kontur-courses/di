using System.Drawing;
using System.Runtime.InteropServices;

namespace TagCloud.App.CloudCreatorDriver.ImageSaver;

public class PngImageSaver : IImageSaver
{
    public bool TrySaveImage(Bitmap image, string fullFileName)
    {
        try
        {
            image.Save(fullFileName);
            return true;
        }
        catch
        {
            return false;
        }
    }
}