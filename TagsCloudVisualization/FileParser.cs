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

        public Dictionary<T, int> GetWordsFrequensy<T>(IEnumerable<T> input)
        {
            return input
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