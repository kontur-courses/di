namespace TagsCloudContainer.Filtering
{
    public class FilterSettings
    {
        public string BlacklistPath { get; }
        

        public FilterSettings(string blacklistPath)
        {
            BlacklistPath = blacklistPath;
        }
    }
}