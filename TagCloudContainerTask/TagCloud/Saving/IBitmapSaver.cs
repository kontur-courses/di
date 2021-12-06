using System.Drawing;

namespace TagCloud.Saving
{
    public interface IBitmapSaver
    {
        string Save(Bitmap bitmap, bool openAfterSave);
    }
}