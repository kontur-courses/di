using System.Drawing;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class TagsCloudHandler
    {
        private string[] words;
        private readonly IWordNormalizer wordNormalizer;
        private readonly IWordsFilter wordsFilter;
        private readonly ITagsCloudDrawer drawer;
        
        public TagsCloudHandler(IWordsFilter filter, IWordNormalizer normalizer, string[] words, ITagsCloudDrawer drawer)
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
            var neededWords = wordsFilter.FilterWords(words)
                .Select(word => wordNormalizer.NormalizeWord(word))
                .ToList();
            var counts = neededWords
                .GroupBy(word => word)
                .Select(group => new Tag(group.Key, group.Count()));
            return drawer.GetTagsCloud(counts);
        }
    }
}
