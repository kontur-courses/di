using System.Drawing;

namespace TagsCloudContainer
{
    public interface IImageSaver
    {
        string Format { get; set; }

        void Save(string path, string name, Bitmap bitmap);
    }
}