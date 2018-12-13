using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NHunspell;

namespace TagsCloudContainer.Formatters
{
    public class ToInitFormFormatter : IWordsFormatter
    {
        
        public List<string> Format(List<string> words)
        {
            var result = new List<string>();
            string path = Path.GetDirectoryName
                              (Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "\\nhunspell";
            var r = new Regex("st:(\\w+[#]?)");
            using (Hunspell hunspell = new Hunspell($"{path}\\ru.aff", $"{path}\\ru.dic"))
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