using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TagsCloudVisualization.TagsCloud;

namespace TagsCloudVisualization.App
{
    public class PictureBoxImageHolder : PictureBox
    {
        public Bitmap OriginalImage { get; set; }

        private void FailIfNotInitialized()
        {
            if (Image == null)
                throw new InvalidOperationException("Call PictureBoxImageHolder.RecreateImage before other method call!");
        }

        public void RecreateImage(TagsCloudSettings tagCloudSettings)
        {
            Image = new Bitmap(tagCloudSettings.ImageSize.Width, tagCloudSettings.ImageSize.Height, PixelFormat.Format24bppRgb);
        }

        public void RecreateImage(Bitmap image)
        {
            OriginalImage = image;
            Image = new Bitmap(image, Size);
        }

        public void SaveImage(string fileName)
        {
            FailIfNotInitialized();
            OriginalImage.Save(fileName, ImageFormat.Png);
        }
    }
}