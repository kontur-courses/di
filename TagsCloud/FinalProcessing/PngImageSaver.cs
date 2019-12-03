using System.Drawing;
using TagsCloud.Interfaces;

namespace TagsCloud.FinalProcessing
{
    public class PngImageSaver : IImageSaver
    {
        public void SaveImage(Image image, string path)
        {
            image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
