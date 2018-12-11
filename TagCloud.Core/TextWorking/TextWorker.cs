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

        public IEnumerable<TagStat> GetTagStats()
        {
            var words = wordsReader.ReadFrom(settings.PathToWords);
            var boringWords = new HashSet<string>(wordsReader.ReadFrom(settings.PathToMutedWords));
            return wordsProcessor.Process(words, boringWords, settings.MaxUniqueWordsCount);
        }
    }
}