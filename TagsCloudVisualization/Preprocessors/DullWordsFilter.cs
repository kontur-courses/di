using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace TagsCloudVisualization.Preprocessors
{
    public class DullWordsFilter : IFilter
    {
        private readonly HashSet<string> dullWords;

        public DullWordsFilter()
        {
            dullWords = new HashSet<string>();
            LoadDullWords();
        }

        private void LoadDullWords()
        {
            var pathToAssemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var pathToDullWords = Path.Combine(
                pathToAssemblyDirectory, "Resources", "dull_words.txt"
            );

            using (var reader = new StreamReader(pathToDullWords))
            {
                while (true)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                        break;
                    dullWords.Add(line.Trim());
                }
            }
        }

        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            foreach (var word in words)
                if (!dullWords.Contains(word))
                    yield return word;
        }
    }
}
