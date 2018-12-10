namespace TagCloud.Core.Settings
{
    public class TextWorkingSettings : ISettings
    {
        public string PathToWords { get; set; }
        public string PathToMutedWords { get; set; }
        public int? MaxUniqueWordsCount { get; set; }
    }
}