using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization
{
    public class WordsExtractor : IWordsExtractor
    {
        public List<string> Extract(string path, IWordsExtractorSettings settings)
        {
            var text = File.ReadAllText(path, Encoding.Default)
                .Replace("\n", " ")
                .Replace("\r", " ")
                .Replace("\t", " ");
            text = settings.StopChars.Aggregate(text, (current, c) => current.Replace(c, ' '));
            var words = text.Split(' ')
                .Where(w => w.Length >= 3 && w != string.Empty && !settings.StopWords.Contains(w))
                .Select(w => w.Trim().ToLowerInvariant()).ToList();
            return words;
        }
    }
}