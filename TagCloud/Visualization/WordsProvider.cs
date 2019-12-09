using System.Collections.Generic;
using System.Linq;
using TagCloud.Infrastructure;
using TagCloud.TextReading;
using TagCloud.WordsPreparation;
using TagCloud.WordsProcessing;

namespace TagCloud.Visualization
{
    public class WordsProvider : IWordsProvider
    {
        private readonly ITextReader textReader;
        private readonly IWordProcessor wordProcessor;
        private readonly IWordSizeSetter wordSizeSetter;
        private readonly PictureConfig pictureConfig;

        public WordsProvider(ITextReader textReader, 
            IWordProcessor wordProcessor, IWordSizeSetter wordSizeSetter, PictureConfig pictureConfig)
        {
            this.textReader = textReader;
            this.wordProcessor = wordProcessor;
            this.wordSizeSetter = wordSizeSetter;
            this.pictureConfig = pictureConfig;
        }

        public IEnumerable<Word> GetWords()
        {
            var rawWords = textReader.ReadWords().ToList();
            var preparedWords = wordProcessor.PrepareWords(rawWords).ToList();
            var sizedWords = wordSizeSetter.GetSizedWords(
                preparedWords, pictureConfig).ToList();
            return sizedWords;
        }
    }
}