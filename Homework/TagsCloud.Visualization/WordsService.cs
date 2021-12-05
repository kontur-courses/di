using System.Linq;
using TagsCloud.Visualization.Models;
using TagsCloud.Visualization.WordsParser;
using TagsCloud.Visualization.WordsReaders;

namespace TagsCloud.Visualization
{
    public class WordsService
    {
        private readonly IWordsReadService wordsReadService;
        private readonly IWordsParser wordsParser;

        public WordsService(IWordsReadService wordsReadService, IWordsParser wordsParser)
        {
            this.wordsReadService = wordsReadService;
            this.wordsParser = wordsParser;
        }

        public Word[] GetWords()
        {
            var text = wordsReadService.Read();

            var parsed = wordsParser.CountWordsFrequency(text);

            return parsed
                .Where(x => x.Value > 1)
                .OrderByDescending(x => x.Value)
                .Select(x => new Word(x.Key, x.Value))
                .ToArray();
        }
    }
}