using System.Drawing;

namespace TagCloud.Saver
{
    public class FileImageSaver : IImageSaver
    {
        public void Save(Image image, string fileName)
        {
            image.Save(fileName);
        }
    }
}