using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TagCloud.Settings;

namespace TagCloud.Forms
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
                throw new Exception("TagCloud hasn't been initialized. Please select words and draw new tag");
        }

        public void RecreateImage(ImageSettings imageSettings)
        {
            Image = new Bitmap(imageSettings.Width, imageSettings.Height, PixelFormat.Format24bppRgb);
        }
        
        public void SaveImage(string fileName, ImageFormat extension)
        {
            try
            {
                FailIfNotInitialized();
                Image.Save(fileName, extension);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}