using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Common.FileReaders;

namespace TagsCloudVisualization.Common.WordFilters
{
    public class CustomFilter : ICustomWordFilter
    {
        private readonly IFileReader fileReader;
        private readonly HashSet<string> excludeWords;

        public CustomFilter(IFileReader fileReader)
        {
            this.fileReader = fileReader;
            excludeWords = new HashSet<string>();
        }

        public void Load(string path)
        {
            foreach (var line in fileReader.ReadLines(path))
            {
                var word = line.Trim().ToLower();
                if (!string.IsNullOrEmpty(word))
                    excludeWords.Add(word);
            }
        }

        public IEnumerable<string> Filter(IEnumerable<string> words)
        {
            return words.Where(word => !excludeWords.Contains(word));
        }
    }
}