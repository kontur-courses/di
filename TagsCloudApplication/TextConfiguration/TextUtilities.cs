using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextConfiguration
{
    public static class TextUtilities
    {
        public static Dictionary<string, int> CountWords(this IEnumerable<string> words)
        {
            return words
                .GroupBy(w => w)
                .ToDictionary(grp => grp.Key, grp => grp.Count());
        }   

        public static Dictionary<string, double> NormalizeByMin(this Dictionary<string, int> countedWords)
        {
            var min = countedWords.Select(pair => pair.Value).Min() * 1.0;
            return countedWords
                .ToDictionary(pair => pair.Key, pair => pair.Value / min);
        }
    }
}
