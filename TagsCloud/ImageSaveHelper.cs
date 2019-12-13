using System;
using System.Drawing;
using System.IO;
using System.Linq;
using TagsCloud.ImageSavers;

namespace TagsCloud
{
    public class ImageSaveHelper
    {
        private readonly IImageSaver[] imageSavers;

        public ImageSaveHelper(IImageSaver[] imageSavers)
        {
            this.imageSavers = imageSavers;
        }

        public void SaveTo(Image image, string filename)
        {
            var fileExtension = Path.GetExtension(filename);
            var imageSaver = imageSavers.FirstOrDefault(p =>
                p.FileExtensions.Any(ext => ext == fileExtension));
            if (imageSaver == null)
                throw new ArgumentException($"Can't select file saver for this file format ({filename})");
            imageSaver.Save(image, filename);
        }
    }
}
