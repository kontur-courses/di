using System.Drawing;

namespace TagsCloudVisualization.Saver
{
    internal class ImageSaver : IImageSaver
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public string FileExtension{ get; set; }


        public void Save(Image image)
        {
            image.Save($"{Path}{FileName}{FileExtension}");
        }

        public ImageSaver(string path, string fileName, string fileExtension)
        {
            Path = path;
            FileName = fileName;
            FileExtension = fileExtension;
        }
    }
}
