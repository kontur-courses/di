using System;

namespace TextConfiguration.WordFilters
{
    public class EmptyWordFilter : IWordFilter
    {
        public bool ShouldFilter(string word)
        {
            return String.IsNullOrWhiteSpace(word);
        }
    }
}
