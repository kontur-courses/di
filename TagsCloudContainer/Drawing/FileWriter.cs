using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Drawing
{
    public class FileWriter
    {
        public IDrawer Drawer { get; }

        public FileWriter(IDrawer drawer)
        {
            Drawer = drawer;
        }

        public void WriteToFIle(string filename, ImageFormat format)
        {
            using (var bitmap = new Bitmap(Drawer.Width, Drawer.Height))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                Drawer.Draw(graphics);
                bitmap.Save(filename, format);
            }
        }
    }
}