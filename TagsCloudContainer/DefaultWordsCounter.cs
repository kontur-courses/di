using System.Collections;
using System.Collections.Generic;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class DefaultWordsCounter : IWordsCounter
    {
        public DefaultWordsCounter()
        {
        }

        public Dictionary<string, int> CountWords(IEnumerable<string> arr)
        {
            var res = new Dictionary<string, int>();
            foreach (var str in arr)
            {
                if (res.ContainsKey(str))
                    res[str] += 1;
                else res[str] = 1;
            }

            return res;
        }
    }
}