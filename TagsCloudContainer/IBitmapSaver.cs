using System.Drawing;

namespace TagsCloudContainer
{
    public interface IBitmapSaver
    {
        void Save(Bitmap bitmap, string filename);

        string[] SupportedExtensions { get; }
    }
}
