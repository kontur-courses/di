using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualisation.Output
{
    public interface IFileResultWriter
    {
        void Save(Image result, ImageFormat format, string path);
    }
}