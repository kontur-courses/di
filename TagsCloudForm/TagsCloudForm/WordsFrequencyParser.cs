using System.Collections.Generic;
using System.Linq;
using TagsCloudForm.CircularCloudLayouterSettings;

namespace TagsCloudForm
{
    public class WordsFrequencyParser :IWordsFrequencyParser
    {
        public Dictionary<string, int> GetWordsFrequency(IEnumerable<string> lines, LanguageEnum language)
        {
            var frequencies = new Dictionary<string, int>();
            lines.ToList().ForEach(line =>
            {
                if (frequencies.ContainsKey(line))
                    frequencies[line] = frequencies[line] + 1;
                else
                    frequencies.Add(line, 1);
            });

            return frequencies;
        }
    }
}
