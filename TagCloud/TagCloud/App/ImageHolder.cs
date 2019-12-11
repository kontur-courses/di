using System;
using System.Drawing;
using System.Windows.Forms;

namespace TagCloud
{
    public class ImageHolder : PictureBox, IImageHolder
    {
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

        public void RecreateImage(ImageSettings imageSettings)
        {
            Image = new Bitmap(imageSettings.Width, imageSettings.Height);
        }

        public void SaveImage(string fileName)
        {
            FailIfNotInitialized();
            Image.Save(fileName);
        }
    }
}
