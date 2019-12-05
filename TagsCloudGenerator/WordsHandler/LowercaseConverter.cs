using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudGenerator.WordsHandler
{
    public class LowercaseConverter : IConverter
    {
        public Dictionary<string, int> Convert(Dictionary<string, int> wordToCount)
        {
            if(wordToCount == null)
                throw new ArgumentNullException();

            var converted = new Dictionary<string, int>();
            
            foreach (var el in wordToCount)
            {
                var newKey = el.Key.ToLower();
                
                if(!converted.ContainsKey(newKey))
                    converted.Add(newKey, el.Value);
                else
                    converted[newKey]++;
            }

            return converted;
        }
    }
}