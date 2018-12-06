using System.Linq;

namespace TagsCloudContainer.Words
{
    public class WordAnalizer
    {
        public WordPack[] WordPacks { get; }

        public WordAnalizer(WordPreprocessing wordPreprocessing)
        {
            WordPacks = wordPreprocessing.Words.GroupBy(w => w).Select(g => new WordPack(g.Key, g.Count())).ToArray();
        }
    }
}
