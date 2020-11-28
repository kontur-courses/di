using System.Collections.Generic;
using System.Linq;

namespace TagCloud
{
    public class FrequencyAnalyzer: IFrequencyAnalyzer
    {
        private Dictionary<string, int> Frequencies = new Dictionary<string, int>();
        private IWordParser Parser;
        
        public FrequencyAnalyzer(IWordParser wordParser)
        {
            Parser = wordParser;
        }


        public Dictionary<string, double> GetFrequencyDictionary(string fileName)
        {
            var words = Parser.GetWords(fileName);
            FillFrequencyDictionary(words);

            return GetNormalFrequencies(words.Length);
        }

        private Dictionary<string, double> GetNormalFrequencies(int count)
        {
            var result = new Dictionary<string, double>();
            foreach (var key in Frequencies.Keys)
            {
                result[key] = (double)Frequencies[key] / count;
            }

            return result;
        }

        private void FillFrequencyDictionary(string[] words)
        {
            foreach (var word in words)
            {
                if (Frequencies.ContainsKey(word))
                {
                    Frequencies[word]++;
                }
                else
                {
                    Frequencies[word] = 1;
                }
            }
        }
    }
}