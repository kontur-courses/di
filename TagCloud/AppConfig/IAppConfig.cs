using TagCloud.ImageProcessing;
using TagCloud.PointGenerator;

namespace TagCloud.AppConfig
{
    public interface IAppConfig
    {
        IPointGenerator pointGenerator { get; set; }
        string inputTextFilePath { get; set; }
        string outputImageFilePath { get; set; }
        IImageSettings imageSettings { get; set; }

    }
}
