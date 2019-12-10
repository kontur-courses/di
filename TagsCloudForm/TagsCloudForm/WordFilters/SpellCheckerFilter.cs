using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NHunspell;
using TagsCloudForm.CircularCloudLayouter;

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
                throw new NotImplementedException();
            return words.Where(x=>checker.Spell(x.ToLower()));
        }

        public IEnumerable<string> Filter(CircularCloudLayouterWithWordsSettings settings, IEnumerable<string> words)
        {
            Hunspell checker;
            if (settings.Language == LanguageEnum.English)
                try
                {
                    checker = new Hunspell(
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\", "en_us.aff"),
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\", "en_us.dic"));
                }
                catch (Exception e)
                {
                    throw new Exception("Не удалось загрузить словари для Hunspell");
                }
            else
                throw new Exception("Chosen Language not supported");
            return words.Where(x => checker.Spell(x.ToLower()));
        }
    }
}
