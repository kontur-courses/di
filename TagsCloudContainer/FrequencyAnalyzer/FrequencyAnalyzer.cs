using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.FreqAnalyzer
{
    public class FrequencyAnalyzer
    {
        private readonly Dictionary<string, int> wordFrequency;

        public FrequencyAnalyzer()
        {
            wordFrequency = new Dictionary<string, int>();
        }

        public void Analyze(string text)
        {
            string[] words = text.Split(new[] { ' ', '.', ',', ';', ':', '!', '?', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                if (wordFrequency.ContainsKey(word.ToLower()))
                {
                    wordFrequency[word.ToLower()]++;
                }
                else
                {
                    wordFrequency.Add(word.ToLower(), 1);
                }
            }
        }

        public void SaveToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (KeyValuePair<string, int> pair in wordFrequency)
                {
                    writer.WriteLine($"{pair.Key}: {pair.Value}");
                }
            }
        }
    }
}
