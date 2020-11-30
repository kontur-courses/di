using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TagsCloudVisualisation.Configuration;

namespace TagsCloudVisualisation.Output
{
    public class FileResultWriter : IResultWriter
    {
        private readonly ImageFormat format;

        public FileResultWriter(ImageFormat format, 
            [InputRequired("Result file path")] string path)
        {
            this.format = format;
            Path = path;
        }

        public string Path { get; }

        public void Save(Image result)
        {
            using var writer = new FileStream(Path, FileMode.Create, FileAccess.ReadWrite);
            result.Save(writer, format);
        }
    }
}