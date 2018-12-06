using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Preprocessing
{
    public class WordsPreprocessor
    {
        //private readonly WordsContainer container;
        private readonly WordsPreprocessorSettings settings;

        public WordsPreprocessor(WordsPreprocessorSettings settings)
        {
            //this.container = container;
            this.settings = settings;
        }

        private IEnumerable<string> ProcessWords(string[] words)
        {
            return words.Select(word => word.ToLower()).Where(word => word.Length > 4 && !settings.ExcludedWords.Contains(word));
                //.Where(word => settings.WordsWhich(word))
                //.Select(word => settings.WordsSelector(word));
        }

        public WordsManager CountWordFrequencies(string[] words)
        {
            if (words == null)
                throw new ArgumentNullException("words must be not null");
            var manager = new WordsManager();
            foreach (var word in ProcessWords(words))
                manager.AddWord(word);
            return manager;
        }
    }
}