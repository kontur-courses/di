using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NHunspell;
using TagsCloudForm.CircularCloudLayouterSettings;

namespace TagsCloudForm.WordFilters
{
    public class SpellCheckerFilter : IWordsFilter
    {
        public IEnumerable<string> Filter(IEnumerable<string> words, LanguageEnum language)
        {
            Hunspell checker;
            if (language == LanguageEnum.English)
                checker = new Hunspell(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\", "en_us.aff"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\", "en_us.dic"));
            else
                throw new Exception("Chosen Language not supported");
            return words.Where(x=>checker.Spell(x.ToLower()));
        }

        public IEnumerable<string> Filter(ICircularCloudLayouterWithWordsSettings settings, IEnumerable<string> words)
        {
            return Filter(words, settings.Language);
        }
    }
}
