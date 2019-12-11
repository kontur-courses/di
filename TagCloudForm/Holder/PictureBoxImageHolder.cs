using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TagCloud.Visualization;

namespace TagCloudForm.Holder
{
    public class PictureBoxImageHolder : PictureBox, IImageHolder
    {
        public PictureBoxImageHolder(ImageSettings imageSettings)
        {
            RecreateImage(imageSettings);
        }

        public void UpdateUi()
        {
            Refresh();
            Application.DoEvents();
        }

        public void RecreateImage(ImageSettings imageSettings)
        {
            Image = new Bitmap(imageSettings.ImageSize.Width, imageSettings.ImageSize.Height);
        }

        public void SaveImage(string fileName)
        {
            Image.Save(fileName, ImageFormat.Png);
        }
    }
}