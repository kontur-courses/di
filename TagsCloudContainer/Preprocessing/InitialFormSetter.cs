using System.Collections.Generic;
using System.IO;
using System.Linq;
using NHunspell;

namespace TagsCloudContainer.Preprocessing
{
    public class InitialFormSetter : IWordsPreprocessor
    {
        private readonly WordsPreprocessorSettings settings;
        private readonly Hunspell hunspell;

        public InitialFormSetter(WordsPreprocessorSettings settings)
        {
            this.settings = settings;
            var separator = Path.DirectorySeparatorChar;
            hunspell = new Hunspell($"NHunspellDictionary{separator}ru_RU.aff", $"NHunspellDictionary{separator}ru_RU.dic");
        }

        public IEnumerable<string> Process(IEnumerable<string> words)
        {
            return !settings.BringInTheInitialForm ? words : words.Select(ToInitialForm);
        }

        private string ToInitialForm(string word)
        {
            var firstForm = hunspell.Stem(word).FirstOrDefault();
            return firstForm ?? word;
        }
    }
}