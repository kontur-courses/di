using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Drawing
{
    public class FileWriter : IWriter
    {
        public Graphics Graphics { get; }

        private readonly Bitmap bitmap;
        private readonly IDrawer drawer;
        private readonly ImageSettings imageSettings;

        public FileWriter(IDrawer drawer, ImageSettings imageSettings)
        {
            this.drawer = drawer;
            this.imageSettings = imageSettings;
            bitmap = new Bitmap(imageSettings.Size.Width, imageSettings.Size.Height);
            Graphics =  Graphics.FromImage(bitmap);
        }


        public void WriteToFile(string filename, ImageFormat format)
        {
                drawer.Draw(Graphics);
                bitmap.Save(filename, format);
        }
    }
}