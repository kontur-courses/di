using System.Collections.Generic;
using TagCloud.Core.Settings;
using TagCloud.Core.TextWorking.WordsProcessing;
using TagCloud.Core.TextWorking.WordsReading;
using TagCloud.Core.Util;

namespace TagCloud.Core.TextWorking
{
    public class TextWorker
    {
        private readonly IWordsReader wordsReader;
        private readonly IWordsProcessor wordsProcessor;
        private readonly TextWorkingSettings settings;

        public TextWorker(IWordsReader wordsReader, IWordsProcessor wordsProcessor, TextWorkingSettings settings)
        {
            this.wordsReader = wordsReader;
            this.wordsProcessor = wordsProcessor;
            this.settings = settings;
        }

        public IEnumerable<TagStat> GetTagStats(string pathToWords, string pathToBoringWords = null)
        {
            var words = wordsReader.ReadFrom(pathToWords);
            var boringWords = pathToBoringWords == null
                ? new HashSet<string>()
                : new HashSet<string>(wordsReader.ReadFrom(pathToBoringWords));
            return wordsProcessor.Process(words, boringWords, settings.MaxUniqueWordsCount);
        }
    }
}