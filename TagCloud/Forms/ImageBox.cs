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
            {
                MessageBox.Show("TagCloud hasn't been initialized. Please select words and draw new tag");
                Image = new Bitmap(100, 100, PixelFormat.Format24bppRgb);
            }
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