using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloudApplication.Readers;
using TagCloudApplication.Savers;

namespace TagCloudApplication.WordKeepers
{
    public class SimpleWordKeeper : IWordKeeper
    {
        private readonly string[] delimiters;
        protected readonly IReader reader;

        public SimpleWordKeeper(string[] delimiters, IReader reader)
        {
            this.delimiters = delimiters;
            this.reader = reader;
        }

        public List<(string Word, int Freq)> GetWordIncidenceInPercent(string fileName, int minPossibleWordFrequency = 5)
        {
            var words = PreprocessWords(reader.GetText(fileName));
            return GetWordsFrequencyInPercent(words, minPossibleWordFrequency);
        }

        protected string[] PreprocessWords(string text)
        {
            return text
                .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                .Select(w => w.ToLower()).ToArray();
        }

        protected List<(string, int)> GetWordsFrequencyInPercent(string[] words, int minWordFreq)
        {
            var wordsFreq = CalculateWordsFrequency(words);
            var wAmount = RemoveWordsWithSmallFreq(words.Length, wordsFreq, minWordFreq);

            return wordsFreq.Select(p => (p.Key, p.Value / wAmount * 100)).ToList();

        }

        private int RemoveWordsWithSmallFreq(int wordsAmount, Dictionary<string, int> wordsFreq, int minWordFreq)
        {
            var newWordsAmount = wordsAmount;
            foreach (var pair in wordsFreq)
            {
                if(pair.Value / wordsAmount * 100 >= minWordFreq) continue;
                wordsFreq.Remove(pair.Key);
                newWordsAmount -= pair.Value;
            }

            return newWordsAmount;
        }

        private Dictionary<string, int> CalculateWordsFrequency(string[] words)
        {
            var resDick = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (resDick.ContainsKey(word))
                    resDick[word]++;
                else
                    resDick.Add(word, 1);
            }

            return resDick;
        }
    }
}
