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
        private readonly AppSettings appSettings;

        public PictureBoxImageHolder(AppSettings appSettings)
        {
            this.appSettings = appSettings;
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

        public void RecreateImage()
        {
            Image = new Bitmap(appSettings.ImageSettings.Width,
                appSettings.ImageSettings.Height, PixelFormat.Format24bppRgb);
        }

        public void SaveImage(string fileName)
        {
            FailIfNotInitialized();
            Image.Save(fileName, ImageFormat.Png);
        }

        private void FailIfNotInitialized()
        {
            if (Image == null)
                throw new InvalidOperationException(
                    "Call PictureBoxImageHolder.RecreateImage before other method call!");
        }
    }
}