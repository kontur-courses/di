using System.Collections.Generic;

namespace TagsCloudContainer
{
    public class BoringWordsDeleter
    {
        public Dictionary<string, int> DeleteBoringWords(Dictionary<string, int> words)
        {
            var res = new Dictionary<string, int>();
            foreach (var pair in words)
            {
                var word = pair.Key;
                var count = pair.Value;
                if (word.Length > 3)
                    res.Add(word, count);
            }

            return res;
        }
    }
}