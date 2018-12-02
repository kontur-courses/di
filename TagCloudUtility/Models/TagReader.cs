using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Models;

namespace TagCloud.Utility.Models
{
    public class TagReader
    {
        public readonly TagGroups TagGroups;

        public TagReader(TagGroups tagGroups)
        {
            TagGroups = tagGroups;
        }

        public List<TagItem> GetTags(string pathToWords)
        {
            var words = WordReader
                .ReadAllWords(pathToWords)
                .Select(word => word.ToLower());
            var frequencyDictionary = GetFrequencyOfWord(words);

            return GetTagItems(frequencyDictionary);
        }

        /// <summary>
        /// Return list of tag items
        /// </summary>
        /// <param name="frequencyDictionary"> Words with their frequency</param>
        /// <returns></returns>
        private List<TagItem> GetTagItems(Dictionary<string, int> frequencyDictionary)
        {
            var items = new List<TagItem>();
            var maxRepeats = frequencyDictionary.Values.Max();
            foreach (var group in TagGroups.Groups)
            {
                var sizeGroup = group.Value;
                var wordsInGroup = frequencyDictionary
                    .Where(pair => sizeGroup.Contains((double)pair.Value / maxRepeats))
                    .Select(pair => pair.Key);
                var tags = wordsInGroup
                    .Select(word => new TagItem(word, sizeGroup.GetSizeForWord(word)))
                    .ToList();
                items.AddRange(tags);
            }

            return items;
        }

        private static Dictionary<string, int> GetFrequencyOfWord(IEnumerable<string> words)
        {
            var frequencyDictionary = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (frequencyDictionary.ContainsKey(word))
                    frequencyDictionary[word]++;
                else
                    frequencyDictionary.Add(word, 1);
            }

            return frequencyDictionary;
        }
    }
}