using System.Drawing;

namespace TagsCloudContainer
{
    public interface IImageSaver
    {
        public string Format { get; set; }

        public void Save(string path, string name, Bitmap bitmap);
    }
}