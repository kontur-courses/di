using System.Drawing;

namespace TagCloudContainer.Infrastructure.Saver;

public interface IImageSaver
{
    void Save(Bitmap bitmap);
}