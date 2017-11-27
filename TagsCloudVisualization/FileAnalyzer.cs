using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using NHunspell;
using NUnit.Framework;

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
            using (var hunspell = new Hunspell("en_US.aff", "en_US.dic"))
            {
                return input.SelectMany(line => Regex.Split(line, @"[^\p{L}]*\p{Z}[^\p{L}]*"))
                    .Select(x =>
                    {
                        var word = x.ToLower();
                        var stems = hunspell.Stem(word);
                        return stems.Any() ? stems[0] : word;
                    })
                    .Where(word => word.Length > minLength)
                    .GroupBy(word => word)
                    .OrderByDescending(x => x.Count())
                    .Take(count)
                    .ToDictionary(x => x.Key, x => x.Count());
            }
        }
        
    }

    [TestFixture]
    public class FileAnalyzer_Should
    {
        [Test]
        
        public void DoSomething_WhenSomething()
        {
            using (var hunspell = new Hunspell(
                TestContext.CurrentContext.TestDirectory+"\\en_US.aff", 
                TestContext.CurrentContext.TestDirectory+"\\en_US.dic"))
            {
                var stems = hunspell.Stem("decompressed");
                var actualWord = stems[0];
                var expected = "compress";
                actualWord.Should().Be(expected);
            }
        }
    }
}