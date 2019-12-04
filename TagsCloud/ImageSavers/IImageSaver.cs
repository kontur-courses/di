using System.Drawing;

namespace TagsCloud.ImageSavers
{
    public interface IImageSaver
    {
        string[] FileExtensions { get; }
        void Save(Image image, string filename);
    }
}
