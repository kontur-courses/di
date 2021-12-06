using System.Drawing;

namespace TagCloudTask
{
    public interface ITagCloud
    {
        void PutNextTag(Size tagSize);

        string SaveBitmap(bool shouldShowLayout, bool shouldShowMarkup, bool openAfterSave);
    }
}