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
        private AppSettings appSettings = AppSettings.Default;

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

        public void RecreateImage(AppSettings settings)
        {
            appSettings = settings;
            Image = new Bitmap(appSettings.ImageSettings.Width,
                appSettings.ImageSettings.Height, PixelFormat.Format24bppRgb);
        }

        public void SaveImage(string fileName)
        {
            FailIfNotInitialized();
            Image.Save(fileName, ImageFormat.Png);
        }

        public AppSettings GetAppSettings()
        {
            return appSettings;
        }

        private void FailIfNotInitialized()
        {
            if (Image == null)
                throw new InvalidOperationException(
                    "Call PictureBoxImageHolder.RecreateImage before other method call!");
        }
    }
}