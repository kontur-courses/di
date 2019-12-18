using System.Collections.Generic;
using TagCloud.App;
using TagCloud.Infrastructure;

namespace TagCloud.WordsProcessing
{
    public class WordClassBasedSelector : IWordSelector
    {
        private readonly IWordClassIdentifier wordClassIdentifier;
        private readonly ISettingsProvider settingsProvider;
        private HashSet<WordClass> WordClasses => settingsProvider.GetSettings().WordClassSettings.WordClasses;
        private bool IsBlackList => settingsProvider.GetSettings().WordClassSettings.IsBlackList;

        public WordClassBasedSelector(
            IWordClassIdentifier wordClassIdentifier, 
            ISettingsProvider settingsProvider)
        {
            this.wordClassIdentifier = wordClassIdentifier;
            this.settingsProvider = settingsProvider;
        }

        public bool IsSelectedWord(Word word)
        {
            var wordClass = wordClassIdentifier.GetWordClass(word.Value);
            word.SetWordClass(wordClass);
            if (IsBlackList)
                return !WordClasses.Contains(wordClass);
            return WordClasses.Contains(wordClass);
        }
    }
}
