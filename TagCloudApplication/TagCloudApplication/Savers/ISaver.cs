using System.Drawing;

namespace TagCloudApplication.Savers
{
    public interface ISaver
    {
        void Save(string fileName, Bitmap image);
    }
}