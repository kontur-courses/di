using System.Drawing;
using System.IO;

namespace TagsCloud.App
{
    public class ImageHolder
    {
        private readonly ImageSaverProvider imageSaverProvider;
        private Image image;

        public ImageHolder(ImageSaverProvider imageSaverProvider)
        {
            this.imageSaverProvider = imageSaverProvider;
        }

        public Graphics StartDrawing()
        {
            return Graphics.FromImage(image);
        }

        public void RecreateImage(ImageSize imageSize)
        {
            image = new Bitmap(imageSize.Width, imageSize.Height);
        }

        public void SaveImage(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            imageSaverProvider.GetImageSaver(extension).Save(image, fileName);
        }
    }
}