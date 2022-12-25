using System.Drawing.Imaging;
using TagCloudGUI.Interfaces;
using TagCloudGUI.Settings;

namespace TagCloudGUI
{
    public class PictureBoxTags : PictureBox, IImageSettingsProvider
    {
        public Size GetImageSize()
        {
            return Image == null
                ? throw new InvalidOperationException(
                    "Call PictureBoxImageHolder.RecreateImage before other method call!")
                : Image.Size;

        }

        public Graphics StartDrawing()
        {
            return Image == null
                ? throw new InvalidOperationException(
                    "Call PictureBoxImageHolder.RecreateImage before other method call!")
                : Graphics.FromImage(Image);
        }

        public void UpdateUi()
        {
            Refresh();
            Application.DoEvents();
        }

        public void RecreateImage(ImageSettings imageSettings)
        {
            Image = new Bitmap(imageSettings.Width, imageSettings.Height, PixelFormat.Format24bppRgb);
        }

        public void SaveImage(string fileName)
        {
            if (Image == null)
                throw new InvalidOperationException(
                    "Call PictureBoxImageHolder.RecreateImage before other method call!");
            Image.Save(fileName);
        }
    }
}
