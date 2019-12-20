using System.Drawing;
using TagsCloudVisualization.Results;

namespace TagsCloudVisualization.ImageSaver
{
    public interface IImageSaver
    {
        Result<None> Save(Bitmap bitmap, string pathToSave);
    }
}