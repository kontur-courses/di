using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudApp.ToSizeConverter
{
    public class WordToSizeConverter: IToSizeConverter
    {
        public IEnumerable<Tuple<string, Size>> ConvertToSizes(IEnumerable<string> words)
        {
            var frequencyDictionary = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!frequencyDictionary.ContainsKey(word))
                    frequencyDictionary[word] = 0;
                frequencyDictionary[word]++;
            }

            frequencyDictionary.OrderByDescending(p => p.Value).ToDictionary(p => p.Key, p => p.Value);
            var defaultSize = new Size(30, 30);
            var sizes = new List<Tuple<string, Size>>();
            foreach (var word in frequencyDictionary.Keys)
            {
                var coef = frequencyDictionary[word];
                if (coef > 6)
                    coef = coef / 5;
                var size = new Size((int)(defaultSize.Width * word.Length * 0.7 * coef), defaultSize.Height * coef);                
                sizes.Add(new Tuple<string, Size>(word, size));
            }        
            return sizes;
        }
    }
}