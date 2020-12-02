using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloud.App
{
    public class TagscloudHandler
    {
        private string[] words;
        public readonly TagscloudSettings Settings;
        public HashSet<string> excludedWords { get; private set; }
        private readonly IWordsConverter wordConverter;
        private readonly ITagscloudDrawer drawer;

        public TagscloudHandler(IWordsConverter converter, string[] words,
            HashSet<string> excludedWords, TagscloudSettings settings, ITagscloudDrawer drawer)
        {
            wordConverter = converter;
            this.words = words;
            this.excludedWords = excludedWords;
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
                .Where(word => !excludedWords.Contains(word))
                .ToList();
            var counts = new WordsCounter().CountWords(neededWords);
            return drawer.GetTagscloud(counts, Settings, 0.7d);
        }
    }
}
