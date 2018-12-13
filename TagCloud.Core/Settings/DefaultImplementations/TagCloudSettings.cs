using TagCloud.Core.Settings.Interfaces;

namespace TagCloud.Core.Settings.DefaultImplementations
{
    public class TagCloudSettings : ITagCloudSettings
    {
        public string PathToWords { get; set; }
        public string PathToBoringWords { get; set; }
        public string PathForResultImage { get; set; } = "result.png";
    }
}