using System.Drawing;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class TagsCloudHandler
    {
        private readonly ITagsCloudDrawer drawer;
        private readonly IWordNormalizer wordNormalizer;
        private readonly IWordsFilter wordsFilter;
        private string[] words;

        public TagsCloudHandler(IWordsFilter filter, IWordNormalizer normalizer, string[] words,
            ITagsCloudDrawer drawer)
        {
            wordNormalizer = normalizer;
            wordsFilter = filter;
            this.words = words;
            this.drawer = drawer;
        }

        public void SetWords(string[] newWords)
        {
            words = newWords;
        }

        public Image GetNewTagcloud()
        {
            var neededWords = wordsFilter.FilterWords(words
                    .Select(word => wordNormalizer.NormalizeWord(word))
                )
                .ToList();
            var counts = neededWords
                .GroupBy(word => word)
                .Select(group => new Word(group.Key, group.Count()));
            return drawer.GetTagsCloud(counts);
        }
    }
}