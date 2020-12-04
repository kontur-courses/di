using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App
{
    public class PictureBoxImageHolder : PictureBox, IImageHolder
    {
        private ImageSettings imageSettings = ImageSettings.GetDefaultSettings();

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

        public void UpdateUi()
        {
            Refresh();
            Application.DoEvents();
        }

        public void RecreateImage(ImageSettings imageSettings)
        {
            this.imageSettings = imageSettings;
            Image = new Bitmap(imageSettings.ImageSize.Width,
                imageSettings.ImageSize.Height, PixelFormat.Format24bppRgb);
        }

        public void SaveImage(string fileName)
        {
            FailIfNotInitialized();
            Image.Save(fileName);
        }

        public ImageSettings GetImageSettings()
        {
            return imageSettings;
        }

        private void FailIfNotInitialized()
        {
            if (Image == null)
                throw new InvalidOperationException(
                    "Call PictureBoxImageHolder.RecreateImage before other method call!");
        }
    }
}