using System.Drawing;

namespace TagCloudApplication
{
    public interface ISaver
    {
        void Save(string fileName, Bitmap image);
    }
}