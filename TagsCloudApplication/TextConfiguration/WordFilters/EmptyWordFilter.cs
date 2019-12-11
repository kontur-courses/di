using System;

namespace TextConfiguration.WordFilters
{
    public class EmptyWordFilter : IWordFilter
    {
        public bool ShouldExclude(string word)
        {
            return String.IsNullOrWhiteSpace(word);
        }
    }
}
