using System.Drawing;
using System.Runtime.InteropServices;
using TagCloud.App.CloudCreatorDriver.ImageSaver.FileTypes;

namespace TagCloud.App.CloudCreatorDriver.ImageSaver;

public class PngImageSaver : IImageSaver
{
    public bool TrySaveImage(Bitmap image, IFileType fileType)
    {
        try
        {
            image.Save(fileType.Path);
            return true;
        }
        catch (ArgumentNullException)
        {
            return false;
        }
        catch (ExternalException)
        {
            return false;
        }
    }
}