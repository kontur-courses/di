using System.Drawing;

namespace TagsCloudContainer.ResultProcessing
{
    public interface IResultProcessor
    {
        void ProcessResult(Bitmap bitmap);
    }
}