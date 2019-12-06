using System.Drawing;

namespace TagsCloudContainer.Vizualization
{
    public interface ISaver
    {
        void SaveImage(string path, Bitmap bitmap);
    }
}