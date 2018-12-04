using System.Collections.Generic;
using System.Linq;
using TagCloud.Models;

namespace TagCloud.Utility.Models.Tag
{
    public class TagReader : ITagReader
    {
        private readonly TagContainer _tagContainer;

        public TagReader(TagContainer tagContainer)
        {
            this._tagContainer = tagContainer;
        }

        public List<TagItem> GetTags(string[] words)
        {
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
            foreach (var group in _tagContainer)
            {
                var wordsInGroup = frequencyDictionary
                    .Where(pair => group.Contains((double)pair.Value / maxRepeats))
                    .Select(pair => pair.Key);
                var tags = wordsInGroup
                    .Select(word => new TagItem(word, group.GetSizeForWord(word)))
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