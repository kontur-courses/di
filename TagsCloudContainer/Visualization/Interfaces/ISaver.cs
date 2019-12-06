using System.Drawing;

namespace TagsCloudContainer.Vizualization.Interfaces
{
    public interface ISaver
    {
        void SaveImage(string path, Bitmap bitmap);
    }
}