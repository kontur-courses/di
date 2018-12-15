using System.Collections.Generic;
using System.Linq;
using TagCloud.Models;
using TagCloud.Utility.Models.Tag.Container;

namespace TagCloud.Utility.Models.Tag
{
    public class TagReader : ITagReader
    {
        private readonly ITagContainer tagContainer;

        public TagReader(ITagContainer tagContainer)
        {
            this.tagContainer = tagContainer;
        }

        public List<TagItem> ReadTags(IEnumerable<string> words)
        {
            var frequencyDictionary = GetFrequencyOfWords(words);

            return GetTagItems(frequencyDictionary);
        }

        private List<TagItem> GetTagItems(IDictionary<string, int> frequencyDictionary)
        {
            var maxRepeats = frequencyDictionary.Values.Max();

            return frequencyDictionary
                .Select(pair => GetTagItem(pair, maxRepeats))
                .ToList();
        }

        private TagItem GetTagItem(KeyValuePair<string, int> pair, double maxRepeats)
        {
            var tagGroup = tagContainer.GetTagGroupFor(pair.Value / maxRepeats);
            return new TagItem(pair.Key, tagGroup.FontSize);
        }

        private static Dictionary<string, int> GetFrequencyOfWords(IEnumerable<string> words)
        {
            return words
                .GroupBy(word => word)
                .ToDictionary(word => word.Key, word => word.Count());
        }
    }
}