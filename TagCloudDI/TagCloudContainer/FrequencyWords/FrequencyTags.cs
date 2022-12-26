using TagCloudContainer.Interfaces;
using TagCloudContainer.Models;

namespace TagCloudContainer.FrequencyWords
{
    public class FrequencyCounter : IFrequencyCounter
    {
        public IEnumerable<TagWithFrequency> GetTagsFrequency(IEnumerable<string> words, IFrequencySorter frequencySorter)
        {
            if (words == null)
                throw new ArgumentNullException(nameof(words));

            var totalWords = 0;
            var frequencyDict = new Dictionary<string, int>();

            foreach (var word in words)
            {
                totalWords++;

                if (frequencyDict.ContainsKey(word))
                    frequencyDict[word]++;
                else
                    frequencyDict.Add(word, 1);
            }


            frequencyDict = frequencySorter.GetSortedWordsWithFrequancies(frequencyDict);

            return frequencyDict
                .Select(pair => new TagWithFrequency(pair.Key, pair.Value));
        }
    }
}