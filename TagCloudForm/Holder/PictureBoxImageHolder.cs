using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TagCloud;

namespace TagCloudForm.Holder
{
    public class PictureBoxImageHolder : PictureBox, IImageHolder
    {
        public PictureBoxImageHolder(ImageSettings imageSettings)
        {
            RecreateImage(imageSettings);
        }

        public Size GetImageSize()
        {
            return Image.Size;
        }

        public Graphics StartDrawing()
        {
            return Graphics.FromImage(Image);
        }

        public void UpdateUi()
        {
            Refresh();
            Application.DoEvents();
        }

        public void RecreateImage(ImageSettings imageSettings)
        {
            Image = new Bitmap(imageSettings.Width, imageSettings.Height);
            BackColor = Color.Black;
        }

        public void SaveImage(string fileName)
        {
            Image.Save(fileName, ImageFormat.Png);
        }
    }
}