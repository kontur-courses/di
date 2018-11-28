using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TagCloud.Settings;

namespace TagCloud
{
    public class ImageBox : PictureBox
    {
        public Size GetImageSize()
        {
            FailIfNotInitialized();
            return Image.Size;
        }

        public Graphics StartDrawing()
        {
            FailIfNotInitialized();
            return Graphics.FromImage(Image);
        }

        private void FailIfNotInitialized()
        {
            if (Image == null)
                throw new InvalidOperationException(
                    "Call    ImageBox.RecreateImage    before    other    method    call!");
        }

        public void RecreateImage(ImageSettings imageSettings)
        {
            Image = new Bitmap(imageSettings.Width, imageSettings.Height, PixelFormat.Format24bppRgb);
        }

        public void SaveImage(string fileName)
        {
            FailIfNotInitialized();
            Image.Save(fileName);
        }
    }
}