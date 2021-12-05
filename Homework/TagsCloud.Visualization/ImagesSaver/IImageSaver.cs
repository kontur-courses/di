using System.Drawing;

namespace TagsCloud.Visualization.ImagesSaver
{
    public interface IImageSaver
    {
        void Save(Image image);
    }
}