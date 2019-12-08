using System.Collections.Generic;

namespace TagCloud.TextFilter
{
    public class FrequencyDictionaryMaker
    {
        private readonly TextFilter textFilter;

        public FrequencyDictionaryMaker(TextFilter textFilter)
        {
            this.textFilter = textFilter;
        }

        public Dictionary<string, int> MakeFrequencyDictionary()
        {
            var result = new Dictionary<string, int>();
            foreach (var word in textFilter.FilterWords())
                if (!result.ContainsKey(word))
                    result[word] = 1;
                else result[word]++;

            return result;
        }
    }
}