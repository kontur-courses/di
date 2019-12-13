using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.GUI
{
    public class PictureBoxImageHolder : PictureBox
    {
        public Size GetImageSize()
        {
            return Image.Size;
        }

        public void SetImageSize(ImageSettings imageSettings)
        {
            Image = new Bitmap(imageSettings.Width, imageSettings.Height);
        }

        public void UpdateUi()
        {
            Refresh();
            Application.DoEvents();
        }

        public void SetImage(Bitmap bitmap)
        {
            Image = bitmap;
        }

        public void SaveImage(string fileName, ImageFormat format)
        {
            Image.Save(fileName, format);
        }
    }
}