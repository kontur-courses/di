using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TagsCloudVisualization.Infrastructure;
using TagsCloudVisualization.Infrastructure.Common;

namespace TagsCloudVisualization.App
{
    public sealed class TagsCloudPictureHolder : PictureBox
    {
        private ImageSettings ImageSettings { get; }

        public TagsCloudPictureHolder(ImageSettings imageImageSettings)
        {
            ImageSettings = imageImageSettings;
            Dock = DockStyle.Fill;
            SizeMode = PictureBoxSizeMode.StretchImage;
            Image = new Bitmap(imageImageSettings.Width, imageImageSettings.Height, PixelFormat.Format24bppRgb);
        }

        public void SaveImage(string fileName) => Image.Save(fileName);

        public void RecreateImage() =>
            Image = new Bitmap(ImageSettings.Width, ImageSettings.Height, PixelFormat.Format24bppRgb);

        public void UpdateUi()
        {
            Refresh();
            Application.DoEvents();
        }
    }
}