using System.Collections.Generic;

namespace TagsCloudContainer.WordFilter
{
    public class BoringWordFilter : IFilter
    {
        private readonly List<string> boringWords;

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