using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHunspell;

namespace TagsCloudForm
{
    public class SpellCheckerFilter
    {
        public IEnumerable<string> Filter(string[] words, LanguageEnum language)
        {
            Hunspell checker;
            if (language == LanguageEnum.English)
                checker = new Hunspell(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\", "en_us.aff"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\", "en_us.dic"));
            else
                throw new NotImplementedException();
            return words.Where(x=>checker.Spell(x));
        }
    }
}
