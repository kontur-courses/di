using System.Drawing;

namespace TagsCloudContainer.Visualization.Interfaces
{
    public interface ISaver
    {
        void SaveImage(string path, Bitmap bitmap, Size resolution);
    }
}