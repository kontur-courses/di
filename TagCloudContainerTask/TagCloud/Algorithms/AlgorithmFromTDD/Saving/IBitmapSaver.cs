using System.Drawing;

namespace TagCloud.Algorithms.AlgorithmFromTDD.Saving
{
    public interface IBitmapSaver
    {
        string Save(Bitmap bitmap, bool openAfterSave);
    }
}