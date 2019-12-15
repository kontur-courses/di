using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Interfaces
{
    public interface IFileSaver
    {
        void Save(Bitmap bitmap, string path);
    }
}