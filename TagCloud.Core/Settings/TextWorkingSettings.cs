namespace TagCloud.Core.Settings
{
    public class TextWorkingSettings : ISettings
    {
        public string PathToWords { get; set; }
        public string PathToBoringWords { get; set; }
        public int? MaxUniqueWordsCount { get; set; }
    }
}