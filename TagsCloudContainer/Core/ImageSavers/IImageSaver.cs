using System.Drawing;

namespace TagsCloudContainer.Core.ImageSavers
{
    interface IImageSaver
    {
        void Save(string pathImage, Bitmap bitmap, string format);
    }
}