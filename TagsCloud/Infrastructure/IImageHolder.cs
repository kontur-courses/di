using System.Drawing;
using TagsCloud.App;

namespace TagsCloud.Infrastructure
{
    public interface IImageHolder
    {
        Graphics StartDrawing();

        void RecreateImage(ImageSize imageSize);

        void SaveImage(string fileName);
    }
}