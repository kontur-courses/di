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

        public Result<IEnumerable<string>> Filter(CircularCloudLayouterWithWordsSettings settings, IEnumerable<string> words)
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
                    return new Result<IEnumerable<string>>("Не удалось загрузить словари для Hunspell", words);
                }
            else
                return new Result<IEnumerable<string>>("Chosen Language not supported", words);
            return Result.Ok(words.Where(x => checker.Spell(x.ToLower())));
        }
    }
}
