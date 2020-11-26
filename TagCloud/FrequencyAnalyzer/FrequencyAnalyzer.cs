using System.Collections.Generic;
using System.Linq;

namespace TagCloud
{
    public class FrequencyAnalyzer: IFrequencyAnalyzer
    {
        private Dictionary<string, int> Frequencies = new Dictionary<string, int>();
        private string[] words;
        
        public FrequencyAnalyzer(IWordParser wordParser)
        {
            words = wordParser.GetWords();
        }


        public Dictionary<string, double> GetFrequencyDictionary()
        {
            FillFrequencyDictionary();
            var result = new Dictionary<string, double>();
            foreach (var key in Frequencies.Keys)
            {
                result[key] = (double)Frequencies[key] / words.Length;
            }

            return result;
        }

        private void FillFrequencyDictionary()
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