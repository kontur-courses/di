using System.Drawing;

namespace TagCloud
{
    public interface ICloud
    {
        void PutNextTag(Size tagSize);

        string SaveBitmap(bool shouldShowLayout, bool shouldShowMarkup, bool openAfterSave);
    }
}