using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Drawing
{
    public class FileWriter : IWriter
    {
        public IDrawer Drawer { get; }
        public ImageSettings Settings => Drawer.Layout.Settings;

        public FileWriter(IDrawer drawer)
        {
            Drawer = drawer;
        }

        public void WriteToFile(string filename, ImageFormat format)
        {
            var imageSize = Settings.Size;
            using (var bitmap = new Bitmap(imageSize.Width, imageSize.Height))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                Drawer.Draw(graphics);
                bitmap.Save(filename, format);
            }
        }
    }
}