using System.Drawing;

namespace TagCloud
{
    public interface ISaveImageAction
    {
        void Perform(string path, Bitmap image);
    }
}