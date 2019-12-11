using System.Collections.Generic;
using System.Linq;
using NHunspell;

namespace TagsCloudGenerator.Core.Normalizers
{
    public class WordsNormalizer : IWordsNormalizer
    {
        public IEnumerable<string> GetNormalizedWords(IEnumerable<string> text, HashSet<string> boringWords,
            Hunspell hunspell)
        {
            return from line in text
                from word in line
                    .Split()
                    .Select(TrimPunctuation)
                    .Where(w => w.Length > 0)
                let stemResult = hunspell.Stem(word)
                select stemResult.Count > 0 ? stemResult[0] : word.ToLower()
                into normalizedWord
                where hunspell.Spell(normalizedWord) && !boringWords.Contains(normalizedWord)
                select normalizedWord;
        }

        private static string TrimPunctuation(string str)
        {
            var countPunctuationFromStart = str.TakeWhile(char.IsPunctuation).Count();
            var countPunctuationFromEnd = str.Reverse().TakeWhile(char.IsPunctuation).Count();
            
            if (countPunctuationFromStart == str.Length &&
                countPunctuationFromEnd == str.Length)
            {
                return "";
            }

            return str.Substring(countPunctuationFromStart,
                str.Length - countPunctuationFromEnd - countPunctuationFromStart);
        }
    }
}