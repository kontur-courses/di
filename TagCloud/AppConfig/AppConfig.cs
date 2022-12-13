using TagCloud.ImageProcessing;
using TagCloud.PointGenerator;

namespace TagCloud.AppConfig
{
    internal class AppConfig : IAppConfig
    {
        public IPointGenerator pointGenerator { get; set; }
        public string inputTextFilePath { get; set; }
        public string outputImageFilePath { get; set; }
        public IImageSettings imageSettings { get; set; }

        public AppConfig(IPointGenerator pointGenerator,
                         string inputTextFilePath, 
                         string outputImageFilePath, 
                         IImageSettings imageSettings)
        {
            this.pointGenerator = pointGenerator;
            this.inputTextFilePath = inputTextFilePath;
            this.outputImageFilePath = outputImageFilePath;
            this.imageSettings = imageSettings;
        }
    }
}
 