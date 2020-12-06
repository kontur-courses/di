using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.Settings;

namespace TagsCloudContainer.App
{
    public class PictureBoxImageHolder : PictureBox, IImageHolder
    {
        private readonly IImageSizeSettingsHolder sizeSettings;
        private readonly IImageFormatSettingsHolder formatSettings;

        public PictureBoxImageHolder(IImageSizeSettingsHolder sizeSettings, 
            IImageFormatSettingsHolder formatSettings)
        {
            this.sizeSettings = sizeSettings;
            this.formatSettings = formatSettings;
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
            Image = new Bitmap(sizeSettings.Width,
                sizeSettings.Height, PixelFormat.Format24bppRgb);
        }

        public void SaveImage(string fileName)
        {
            FailIfNotInitialized();
            Image.Save(fileName, formatSettings.Format);
        }

        private void FailIfNotInitialized()
        {
            if (Image == null)
                throw new InvalidOperationException(
                    "Call PictureBoxImageHolder.RecreateImage before other method call!");
        }
    }
}