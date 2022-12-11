using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TagCloud.TagCloudVisualizations;
using TagCloudGui.Infrastructure.Common;

namespace TagCloudGui
{
    public class PictureBoxImageHolder : PictureBox, IImageHolder
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
                throw new InvalidOperationException("Call PictureBoxImageHolder.RecreateImage before other method call!");
        }

        public void UpdateUi()
        {
            Refresh();
            Application.DoEvents();
        }

        public void RecreateImage(ITagCloudVisualizationSettings settings)
        {
            if (settings.PictureSize != null)
                Image = new Bitmap(settings.PictureSize.Value.Width, settings.PictureSize.Value.Height,
                    PixelFormat.Format24bppRgb);
        }

        public void SaveImage(string fileName, ImageFormat format)
        {
            FailIfNotInitialized();
            Image.Save(fileName, format);
        }
    }
}