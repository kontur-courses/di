using System.Drawing;
using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Abstractions;

public interface IVisualizer : IService
{
    Bitmap GetBitmap();
}