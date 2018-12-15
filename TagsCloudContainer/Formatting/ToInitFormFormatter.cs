using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using NHunspell;
using TagsCloudContainer.Properties;

namespace TagsCloudContainer.Formatting
{
    public class ToInitFormFormatter : IWordsFormatter
    {
        public List<string> Format(IEnumerable<string> words)
        {
            var result = new List<string>();

            var r = new Regex("st:(\\w+[#]?)");
            var affFileData = Resources.ru_aff;
            var dicFileData = Encoding.UTF8.GetBytes(Resources.ru_dic);
            using (var hunspell = new Hunspell(dictionaryFileData: dicFileData, affixFileData: affFileData))
            {
                foreach (var word in words)
                {
                    var suggestions = hunspell.Analyze(word.ToLower());
                    if (suggestions.Count == 0)
                    {
                        result.Add(word.ToLower());
                        continue;
                    }

                    var w = r.Match(suggestions.First()).Groups[1].Value;
                    result.Add(w);
                }
            }


            return result;
        }
    }
}