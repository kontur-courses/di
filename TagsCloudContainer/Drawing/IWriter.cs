using System.Drawing.Imaging;

namespace TagsCloudContainer.Drawing
{
    public interface IWriter
    {
        IDrawer Drawer { get; }
        void WriteToFile(string filename, ImageFormat format);
    }
}