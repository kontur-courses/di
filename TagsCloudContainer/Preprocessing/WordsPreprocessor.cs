using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public class WordsPreprocessor
    {
        private readonly WordsContainer container;
        private readonly WordsPreprocessorSettings settings;

        public WordsPreprocessor(WordsContainer container, WordsPreprocessorSettings settings)
        {
            this.container = container;
            this.settings = settings;
        }

        private IEnumerable<string> ProcessWords()
        {
            return container.RawWords.Select(word => word.ToLower()).Where(word => word.Length > 4 && !settings.ExcludedWords.Contains(word));
                //.Where(word => settings.WordsWhich(word))
                //.Select(word => settings.WordsSelector(word));
        }

        public WordsManager CountWordFrequencies()
        {
            if (container.RawWords == null)
                throw new InvalidOperationException("You must read words at first");
            var manager = new WordsManager();
            foreach (var word in ProcessWords())
                manager.AddWord(word);
            container.ProcessedWords = manager;
            return manager;
        }
    }
}