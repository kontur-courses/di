using System.Drawing;
using TagCloud.ImageProcessing;

namespace TagCloud.AppConfig
{
    public interface IAppConfig
    {
        string InputTextFilePath { get; set; }
        string OutputImageFilePath { get; set; }
        string CloudForm { get; set; }
        Point CloudCentralPoint { get; set; }
        IImageSettings ImageSettings { get; set; }
    }
}
