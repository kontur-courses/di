namespace TagCloud.Core.Settings
{
    public class TagCloudSettings : ISettings
    {
        public string PathToWords { get; set; }
        public string PathToBoringWords { get; set; }
        public string PathForResultImage { get; set; } = "result.png";

        public string GetSettingsName()
        {
            return "TagCloud settings";
        }
    }
}