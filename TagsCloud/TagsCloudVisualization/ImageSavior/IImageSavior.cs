using System.Drawing;

namespace TagsCloudVisualization.ImageSavior
{
    public interface IImageSavior
    {
        void Save(string filename, Image image);
    }
}