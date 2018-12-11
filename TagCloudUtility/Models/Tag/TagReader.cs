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

            var items = from pair in frequencyDictionary
                        let wordGroup = tagContainer
                            .First(gr => gr.Item2.Contains((double)pair.Value / maxRepeats))
                            .Item2
                        select new TagItem(pair.Key, wordGroup.FontSize);

            return items
                .OrderByDescending(item => item.FontSize)
                .ToList();
        }

        private static Dictionary<string, int> GetFrequencyOfWords(IEnumerable<string> words)
        {
            return words
                .GroupBy(word => word)
                .ToDictionary(word => word.Key, word => word.Count());
        }
    }
}