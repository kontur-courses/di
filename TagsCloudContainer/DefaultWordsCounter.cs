using System.Collections.Generic;

namespace TagsCloudContainer
{
    public class DefaultWordsCounter : IWordsCounter
    {
        private readonly string[] arr;
        
        public DefaultWordsCounter(string[] arr)
        {
            this.arr = arr;
        }
        public IDictionary<string, int> CountWords()
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