using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.Extensions
{
    public static class PictureBoxExtensions
    {
        public static Graphics StartDrawing(this PictureBox source)
        {
            return Graphics.FromImage(source.Image);
        }

        public static void SaveImage(this PictureBox source, string fileName)
        {
            source.Image.Save(fileName);
        }

        public static void UpdateUi(this PictureBox source)
        {
            source.Refresh();
            Application.DoEvents();
        }

        public static void RecreateImage(this PictureBox source, ImageSettings imageSettings)
        {
            source.Image = new Bitmap(imageSettings.Width, imageSettings.Height, PixelFormat.Format24bppRgb);
        }
    }
}
