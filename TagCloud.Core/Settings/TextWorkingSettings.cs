namespace TagCloud.Core.Settings
{
    public class TextWorkingSettings : ISettings
    {
        public int? MaxUniqueWordsCount { get; set; }

        public string GetSettingsName()
        {
            return "TextWorking settings";
        }
    }
}