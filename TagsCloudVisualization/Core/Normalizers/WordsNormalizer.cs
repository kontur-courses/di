using System.Collections.Generic;
using System.Linq;
using NHunspell;

namespace TagsCloudVisualization.Core.Normalizers
{
    public class RussianWordsNormalizer
    {
        private readonly char[] punctuationMarks = {':', '.', ',', ';', '?', '!', ')', '(', '*', '"', '\'', '«', '»'};
        
        public IEnumerable<string> GetNormalizedWords(IEnumerable<string> text, HashSet<string> boringWords,
            Hunspell hunspell)
        {   
            foreach (var line in text)
            {
                foreach (var word in line.Split().Select(w => w.Trim(punctuationMarks)).Where(w => w.Length > 0))
                {
                    var stemResult = hunspell.Stem(word);
                    var normalizedWord = stemResult.Count > 0 ? stemResult[0] : word.ToLower();
                    if (hunspell.Spell(normalizedWord) && !boringWords.Contains(normalizedWord))
                    {
                        yield return normalizedWord;
                    }
                }
            }
        }
    }
}