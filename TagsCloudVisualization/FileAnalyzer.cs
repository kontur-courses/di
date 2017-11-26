using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class FileAnalyzer:IFileAnalyzer
    {
        private readonly int count;
        private readonly int minLength;

        public FileAnalyzer(int count,int minLength = 0)
        {
            this.count = count;
            this.minLength = minLength;
        }

        public Dictionary<string, int> GetWordsFrequensy(IEnumerable<string> input)
        {
            return input
                .SelectMany(line => line.Split(
                    new char[] { ' ', '\t', ',', ';', '?', '\n', '.'},
                    StringSplitOptions.RemoveEmptyEntries))
                .Where(word=>word.Length > minLength)
                .GroupBy(word => word)
                .OrderByDescending(x=>x.Count())
                .Take(count)
                .ToDictionary(x=>x.Key, x=>x.Count());   
        }
        
    }
}