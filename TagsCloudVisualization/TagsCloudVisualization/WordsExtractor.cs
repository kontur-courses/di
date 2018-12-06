using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class WordsExtractor : IWordsExtractor
    {
        public List<string> Extract(string path)
        {
            var text = File.ReadAllText(path, Encoding.Default)
                .Replace("\n", " ")
                .Replace("\r", " ")
                .Replace("\t", " ");
            text = new WordsExtractorSettings().StopChars.Aggregate(text, (current, c) => current.Replace(c, ' '));
            var words = text.Split(' ')
                .Where(w => w.Length >= 3 && w != string.Empty && !new WordsExtractorSettings().StopWords.Contains(w))
                .Select(w => w.Trim().ToLowerInvariant()).ToList();
            return words;
        }
    }
}