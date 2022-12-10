using System.Collections.Generic;
using System.Linq;
using DeepMorphy;
using TagsCloudVisualization.Infrastructure.Parsers;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Infrastructure
{
    public class Analyzer : IAnalyzer
    {
        private readonly AnalyzerSettings settings;
        private IParser parser;
        private string filePath;


        public Analyzer(AnalyzerSettings settings)
        {
            this.settings = settings;
        }

        public Dictionary<string, int> CreateFrequencyDictionary()
        {
                var result = new Dictionary<string, int>();

                foreach (var word in parser.WordParse(filePath).Select(s => s.ToLower()))
                {
                    if (!result.ContainsKey(word))
                        result[word] = 0;
                    result[word]++;
                }

                var a = ExcludeBoringWords(result.Keys).ToHashSet();

                foreach (var word in result.Keys)
                {
                    if (!a.Contains(word))
                    {
                        result.Remove(word);
                    }
                }
                return result;
        }

        private IEnumerable<string> ExcludeBoringWords(IEnumerable<string> words)
        {
            var morph = new MorphAnalyzer();
            return morph
                .Parse(words)
                .Where(m => !m.BestTag.Has("союз")
                            && !m.BestTag.Has("предл")
                            && !m.BestTag.Has("мест")
                            && !m.BestTag.Has("межд")
                            && !m.BestTag.Has("част")).Select(m => m.Text);

        }

        public void SetParser(IParser parser, string filePath)
        {
            this.parser = parser;
            this.filePath = filePath;
        }
    }
}
