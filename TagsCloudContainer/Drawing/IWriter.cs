using System.Drawing;

namespace TagsCloudContainer.Drawing
{
    public interface IWriter
    {
        void Write(Bitmap image, string name);
    }
}
