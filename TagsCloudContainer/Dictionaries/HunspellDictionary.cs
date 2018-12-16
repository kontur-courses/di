using System;
using System.Linq;
using NHunspell;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.Dictionaries
{
    public class HunspellDictionary : IGrammarDictionary, IDisposable
    {
        private readonly Hunspell hunspell;
        private const string PathToDictionaries = "TagsCloudContainer.DictionaryFiles";

        public HunspellDictionary(ITextSettings textSettings, IReaderResources readerResources)
        {
            var locale = textSettings.DictionaryLocale;
            var affixInfo = readerResources.Read($"{PathToDictionaries}.{locale}.aff");
            var dictionaryInfo = readerResources.Read($"{PathToDictionaries}.{locale}.dic");
            hunspell = new Hunspell(affixInfo, dictionaryInfo);
        }

        public bool ContainsWord(string word)
        {
            return hunspell.Spell(word);
        }
        

        public bool TryGetCorrectForm(string word, out string correctForm)
        {
            var suggestions = hunspell.Suggest(word);
            correctForm = suggestions.FirstOrDefault();
            return correctForm != null;
        }

        public void Dispose()
            => hunspell.Dispose();
    }
}