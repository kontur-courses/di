using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.WordFilter
{
    public class BoringWordFilter : IFilter
    {
        private readonly List<string> boringWords;
        public BoringWordFilter(FilterSettings filterSettings)
        {
            if (!string.IsNullOrEmpty(filterSettings.FileForBoringWords))
                boringWords = File.ReadLines(filterSettings.FileForBoringWords).ToList();
            boringWords = new List<string>();
        }

        public BoringWordFilter(List<string> boringWords)
        {
            this.boringWords = boringWords;
        }

        public bool Validate(string word)
        {
            return !boringWords.Contains(word);
        }
    }
}