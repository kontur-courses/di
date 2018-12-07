using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Drawing
{
    public interface IWriter
    {
        Graphics Graphics { get; }
        void WriteToFile(string filename, ImageFormat format);
    }
}