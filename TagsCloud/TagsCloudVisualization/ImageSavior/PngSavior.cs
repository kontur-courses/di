using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudVisualization.ImageSavior
{
    public class PngSavior : IImageSavior
    {
        public void Save(Image image, string filename)
        {
            image.Save(Path.ChangeExtension(filename, "png"), ImageFormat.Png);
        }
    }
}