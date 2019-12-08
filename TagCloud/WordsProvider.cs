using System.Collections.Generic;
using TagCloud.Infrastructure;
using TagCloud.TextReading;
using TagCloud.WordsPreparation;
using TagCloud.WordsProcessing;

namespace TagCloud
{
    public class WordsProvider : IWordsProvider
    {
        private readonly ITextReader textReader;
        private readonly IWordProcessor wordProcessor;
        private readonly IWordCountSetter wordCountSetter;

        public WordsProvider(ITextReader textReader, IWordProcessor wordProcessor, IWordCountSetter wordCountSetter)
        {
            this.textReader = textReader;
            this.wordProcessor = wordProcessor;
            this.wordCountSetter = wordCountSetter;
        }

        public IEnumerable<Word> GetWords()
        {
            var rawWords = textReader.ReadWords();
            var preparedWords = wordProcessor.PrepareWords(rawWords);
            return wordCountSetter.GetCountedWords(preparedWords);
        }
    }
}