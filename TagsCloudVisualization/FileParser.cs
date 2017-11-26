using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    public class FileParser
    {
        private readonly int count;
        private readonly int minLength;

        public FileParser(int count,int minLength = 0)
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
                .Where(word=>word != "CHAPTER")
                .GroupBy(word => word)
                .OrderByDescending(x=>x.Count())
                .Take(count)
                .ToDictionary(x=>x.Key, x=>x.Count());   
        }
        
    }


    [TestFixture]
    public class WordsParser_Should
    {
        [Test]
        public void Frequency_Test()
        {
            var actual = new FileParser(50, 0).GetWordsFrequensy(new List<string>() {"Hello", "Hello", "!"});
        }
    }
}