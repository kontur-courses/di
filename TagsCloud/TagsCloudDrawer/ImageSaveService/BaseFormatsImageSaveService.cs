using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudDrawer.ImageSaveService
{
    public abstract class BaseFormatsImageSaveService : IImageSaveService
    {
        public void Save(string filename, Image image)
        {
            image.Save(Path.ChangeExtension(filename, Extensions), Format);
        }

        protected abstract string Extensions { get; }
        protected abstract ImageFormat Format { get; }
    }
}