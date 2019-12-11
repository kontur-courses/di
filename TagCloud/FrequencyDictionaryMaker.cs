using System.Collections.Generic;

namespace TagCloud
{
    public class FrequencyDictionaryMaker
    {
        public Dictionary<string, int> MakeFrequencyDictionary(List<string> words)
        {
            var result = new Dictionary<string, int>();
            foreach (var word in words)
                if (!result.ContainsKey(word))
                    result[word] = 1;
                else result[word]++;

            return result;
        }
    }
}