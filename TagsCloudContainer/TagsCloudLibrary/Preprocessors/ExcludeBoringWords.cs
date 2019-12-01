using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TagsCloudLibrary.MyStem;

namespace TagsCloudLibrary.Preprocessors
{
    public class ExcludeBoringWords : IPreprocessor
    {
        private readonly IEnumerable<Word.PartOfSpeech> partOfSpeechWhitelist;
        
        public int Priority { get; } = 20;

        public ExcludeBoringWords(IEnumerable<Word.PartOfSpeech> partOfSpeechWhitelist)
        {
            this.partOfSpeechWhitelist = partOfSpeechWhitelist;
        }
        
        public IEnumerable<string> Act(IEnumerable<string> words)
        {
            var mystem = new MyStemProcess();
            var wordsWithGrammar = mystem.GetWordsWithGrammar(words);

            var filteredWords = wordsWithGrammar
                .Where(word => (partOfSpeechWhitelist.Contains(word.Grammar.PartOfSpeech)))
                .Select(word => word.InitialString);

            return filteredWords;
        }
    }
}
