using System.Drawing;
using System.Drawing.Imaging;
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

        public void ProcessResult(Bitmap bitmap, string filePath, ImageFormat imageFormat)
        {
            imageSaver.SaveBitmap(bitmap, filePath, imageFormat);
            resultDisplay.ShowResult(bitmap);
        }
    }
}