using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class TagsCloudHandler
    {
        private string[] words;
        public readonly TagsCloudSettings Settings;
        public HashSet<string> ExcludedWords { get; }
        private readonly IWordsConverter wordConverter;
        private readonly ITagsCloudDrawer drawer;

        public TagsCloudHandler(IWordsConverter converter, string[] words,
            HashSet<string> excludedWords, TagsCloudSettings settings, ITagsCloudDrawer drawer)
        {
            wordConverter = converter;
            this.words = words;
            ExcludedWords = excludedWords;
            Settings = settings;
            this.drawer = drawer;
        }

        public void SetWords(string[] newWords)
        {
            words = newWords;
        }

        public Image GetNewTagcloud()
        {
            var neededWords = wordConverter.ConvertWords(words)
                .Where(word => !ExcludedWords.Contains(word))
                .ToList();
            var counts = new WordsCounter().CountWords(neededWords);
            return drawer.GetTagsCloud(counts, Settings, 0.7d);
        }
    }
}
