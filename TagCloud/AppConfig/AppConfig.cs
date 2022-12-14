using System.Drawing;
using TagCloud.ImageProcessing;

namespace TagCloud.AppConfig
{
    internal class AppConfig : IAppConfig
    {
        public string InputTextFilePath { get; set; }
        public string OutputImageFilePath { get; set; }
        public string CloudForm { get; set; }
        public Point CloudCentralPoint { get; set; }
        public IImageSettings ImageSettings { get; set; }

        public AppConfig(string inputTextFilePath, 
                         string outputImageFilePath,
                         string cloudForm,
                         Point cloudCentralPoint,
                         IImageSettings imageSettings)
        {
            InputTextFilePath = inputTextFilePath;
            OutputImageFilePath = outputImageFilePath;
            CloudForm = cloudForm;
            CloudCentralPoint = cloudCentralPoint;
            ImageSettings = imageSettings;
        }
    }
}
 