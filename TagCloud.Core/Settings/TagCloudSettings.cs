using System.Drawing.Imaging;
using TagCloud.Core.Util;

namespace TagCloud.Core.Settings
{
    public class TagCloudSettings : ISettings
    {
        public string PathToWords { get; set; }
        public string PathToBoringWords { get; set; }
        public string PathForResultImage { get; set; } = "result.png";

        public ImageFormat ImageFormat => ImageFormatResolver.TryResolveFromFileName(PathForResultImage, out var res)
            ? ImageFormat.Png
            : res;

        public string GetSettingsName()
        {
            return "TagCloud settings";
        }
    }
}