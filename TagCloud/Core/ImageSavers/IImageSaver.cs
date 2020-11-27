using System.Drawing;

namespace TagCloud.Core.ImageSavers
{
    public interface IImageSaver
    {
        public void Save(Bitmap bitmap, string fullPath, string format);
    }
}