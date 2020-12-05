using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class TagsCloudHandler
    {
        private readonly ITagsCloudDrawer drawer;
        private readonly IWordNormalizer wordNormalizer;
        private readonly IEnumerable<IWordsFilter> wordsFilters;
        private string[] words;

        public TagsCloudHandler(IEnumerable<IWordsFilter> filters, IWordNormalizer normalizer, string[] words,
            ITagsCloudDrawer drawer)
        {
            wordNormalizer = normalizer;
            wordsFilters = filters;
            this.words = words;
            this.drawer = drawer;
        }

        public void SetWords(string[] newWords)
        {
            words = newWords;
        }

        public Image GetNewTagcloud()
        {
            var neededWords = words.Where(word => wordsFilters.All(filter => filter.Validate(word)))
                .Select(word => wordNormalizer.Normalize(word))
                .ToList();
            var counts = neededWords
                .GroupBy(word => word)
                .Select(group => new Word(group.Key, group.Count()));
            return drawer.GetTagsCloud(counts);
        }
    }
}