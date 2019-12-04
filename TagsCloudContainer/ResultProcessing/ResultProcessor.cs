using System.Drawing;
using TagsCloudContainer.ResultProcessing.ImageSaving;
using TagsCloudContainer.UserInterface;

namespace TagsCloudContainer.ResultProcessing
{
    public class ResultProcessor : IResultProcessor
    {
        private readonly IImageSaver imageSaver;
        private readonly IResultDisplay resultDisplay;

        public ResultProcessor(IImageSaver imageSaver, IResultDisplay resultDisplay)
        {
            this.imageSaver = imageSaver;
            this.resultDisplay = resultDisplay;
        }

        public void ProcessResult(Bitmap bitmap)
        {
            imageSaver.SaveBitmap(bitmap);
            resultDisplay.ShowResult(bitmap);
        }
    }
}