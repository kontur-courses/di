using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NHunspell;

namespace TagsCloudContainer.Formatting
{
    public class ToInitFormFormatter : IWordsFormatter
    {
        
        public List<string> Format(IEnumerable<string> words)
        {
            var result = new List<string>();
            var path = Path.GetDirectoryName
                              (Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "\\nhunspell";
            var r = new Regex("st:(\\w+[#]?)");
            using (var hunspell = new Hunspell($"{path}\\ru.aff", $"{path}\\ru.dic"))
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