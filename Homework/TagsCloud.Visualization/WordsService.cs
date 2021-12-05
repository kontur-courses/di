using System.Linq;
using TagsCloud.Visualization.Models;
using TagsCloud.Visualization.WordsParser;
using TagsCloud.Visualization.WordsReaders;

namespace TagsCloud.Visualization
{
    public class WordsService
    {
        private readonly IFileReadService fileReadService;
        private readonly IWordsParser wordsParser;

        public WordsService(IFileReadService fileReadService, IWordsParser wordsParser)
        {
            this.fileReadService = fileReadService;
            this.wordsParser = wordsParser;
        }

        public Word[] GetWords(string filename)
        {
            var text = fileReadService.Read(filename);

            var parsed = wordsParser.CountWordsFrequency(text);

            return parsed
                .Where(x => x.Value > 1)
                .OrderByDescending(x => x.Value)
                .Select(x => new Word(x.Key, x.Value))
                .ToArray();
        }
    }
}