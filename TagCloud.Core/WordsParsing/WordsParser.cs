using System.Collections.Generic;
using TagCloud.Core.Settings.Interfaces;
using TagCloud.Core.Util;
using TagCloud.Core.WordsParsing.WordsProcessing;
using TagCloud.Core.WordsParsing.WordsReading;

namespace TagCloud.Core.TextParsing
{
    public class WordsParser
    {
        private readonly IWordsReader wordsReader;
        private readonly IWordsProcessor wordsProcessor;
        private readonly ITextParsingSettings settings;

        public WordsParser(IWordsReader wordsReader, IWordsProcessor wordsProcessor, ITextParsingSettings settings)
        {
            this.wordsReader = wordsReader;
            this.wordsProcessor = wordsProcessor;
            this.settings = settings;
        }

        public IEnumerable<TagStat> Parse(string pathToWords, string pathToBoringWords = null)
        {
            var words = wordsReader.ReadFrom(pathToWords);
            var boringWords = pathToBoringWords == null
                ? new HashSet<string>()
                : new HashSet<string>(wordsReader.ReadFrom(pathToBoringWords));
            return wordsProcessor.Process(words, boringWords, settings.MaxUniqueWordsCount);
        }
    }
}