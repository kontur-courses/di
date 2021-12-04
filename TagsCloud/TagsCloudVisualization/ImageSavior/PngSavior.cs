using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudVisualization.ImageSavior
{
    public class PngSavior : IImageSavior
    {
        public void Save(string filename, Image image)
        {
            image.Save(Path.ChangeExtension(filename, "png"), ImageFormat.Png);
        }
    }
}