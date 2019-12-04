using System.Collections.Generic;
using System.Linq;
using TagsCloudForm.CircularCloudLayouter;

namespace TagsCloudForm
{
    public class WordsFrequencyParser :IWordsFrequencyParser
    {
        public Dictionary<string, int> GetWordsFrequency(string[] lines, SpellCheckerFilter filter, LanguageEnum language)
        {
            var filteredLines = filter.Filter(lines, language).ToList();
            var frequencies = new Dictionary<string, int>();
            filteredLines.ForEach(line =>
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
