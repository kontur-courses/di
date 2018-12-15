using TagsCloudContainer.Settings;

namespace TagsCloudContainer.WordFilter
{
    public class LengthWordFilter : IFilter
    {
        public LengthWordFilter(FilterSettings filterSettings)
        {
            this.filterSettings = filterSettings;
        }

        private readonly FilterSettings filterSettings;
        public bool Validate(string word)
        {
            return word.Length > filterSettings.LengthForBoringWord;
        }
    }
}