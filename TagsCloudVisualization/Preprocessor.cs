using System.Collections.Generic;
using System.IO;
using System.Linq;
using DeepMorphy;

namespace TagsCloudVisualization
{
    public static class Preprocessor
    {
        public static IEnumerable<string> Process(string path)
        {
            var words = new List<string>();
            var lines = File.ReadAllLines(path);
            var morph = new MorphAnalyzer(withLemmatization: true);
            var morhsInfo = morph.Parse(lines);
            
            foreach (var morphInfo in morhsInfo)
            {
                var bestTag = morphInfo.BestTag;
            
                if (IsContainsInvalidGrammems(bestTag.Grams))
                    continue;
            
                words.Add(bestTag.HasLemma ? bestTag.Lemma : morphInfo.Text);
            }

            return words;
        }

        private static bool IsContainsInvalidGrammems(IEnumerable<string> grams)
        {
            return grams.Contains("мест") 
                   || grams.Contains("предл") 
                   || grams.Contains("союз") 
                   || grams.Contains("част")
                   || grams.Contains("межд") 
                   || grams.Contains("пункт") 
                   || grams.Contains("цифра")
                   || grams.Contains("рим_цифр");
        }
    }
}