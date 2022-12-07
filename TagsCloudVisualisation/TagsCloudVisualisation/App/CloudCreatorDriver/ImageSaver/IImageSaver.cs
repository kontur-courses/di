using System.Drawing;
using TagsCloudVisualisation.App.ImageSaver.FileTypes;

namespace TagsCloudVisualisation.App.ImageSaver
{
    public interface IImageSaver
    {
        bool TrySaveImage(Bitmap image, IFileType fileType);
    }
}