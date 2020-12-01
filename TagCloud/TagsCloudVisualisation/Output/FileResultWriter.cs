using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudVisualisation.Output
{
    public class FileResultWriter : IFileResultWriter
    {
        private readonly ImageFormat format;

        public FileResultWriter(ImageFormat format)
        {
            this.format = format;
        }

        public void Save(Image result, string path)
        {
            using var writer = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            result.Save(writer, format);
        }
    }
}