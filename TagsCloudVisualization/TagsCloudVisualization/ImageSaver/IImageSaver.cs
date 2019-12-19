using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IImageSaver
    {
        void Save(Bitmap bitmap, string pathToSave);
    }
}