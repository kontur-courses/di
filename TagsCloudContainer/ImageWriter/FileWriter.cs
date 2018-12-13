using System.IO;

namespace TagsCloudContainer.ImageWriter
{
    public class FileWriter : IImageWriter
    {
        public void Write(byte[] image, string imageName, string imageFormat, string pathToSave)
        {
            File.WriteAllBytes(Path.Combine(pathToSave, $"{imageName}.{imageFormat}"), image);
        }
    }
}