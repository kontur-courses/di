using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class WordsGenerator : IWordsGenerator
    {
        private IDictionary<string, int> WordsFrequency { get; set; }
        private IConfiguration Configuration { get; set; }

        public WordsGenerator(IWordCounter wordCounter, IConfiguration configuration)
        {
            WordsFrequency = wordCounter.GetWordsFrequency();
            Configuration = configuration;
        }

        public IEnumerable<IWord> GenerateWords()
        {
            return WordsFrequency.Select(
                x => new Word(x.Key, new Font(Configuration.FontFamily, Configuration.FontSize + (x.Value - 1))));
        }
    }
}
