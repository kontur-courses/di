using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloud.App
{
    public class ImageHolder
    {
        private Image image;

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
            image.Save(fileName, ImageFormat.Png);
        }
    }
}