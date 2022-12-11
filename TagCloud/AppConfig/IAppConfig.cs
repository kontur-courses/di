using TagCloud.ImageProcessing;

namespace TagCloud.AppConfig
{
    public interface IAppConfig
    {
        string inputTextFilePath { get; set; }
        string outputImageFileFullPath { get; set; }
        IImageSettings imageSettings { get; set; }
    }
}
