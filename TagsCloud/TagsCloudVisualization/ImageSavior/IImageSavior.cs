using System.Drawing;

namespace TagsCloudVisualization.ImageSavior
{
    public interface IImageSavior
    {
        void Save(Image image, string filename);
    }
}