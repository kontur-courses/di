using TagsCloudContainer.Core.Options.Interfaces;

namespace TagsCloudContainer.Core.Options
{
    public class FilterOptions : IFilterOptions
    {
        public string MyStemLocation { get; set; }
        public IEnumerable<string> BoringWords { get; set; }

        public FilterOptions(string myStemLocation, IEnumerable<string> boringWords)
        {
            MyStemLocation = myStemLocation;
            BoringWords = boringWords;
        }
    }
}
