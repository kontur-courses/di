using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
//            return input
//                .SelectMany(line => line.Split(
//                    new char[] { ' ', '\t', ',', ';', '?', '\n', '.'},
//                    StringSplitOptions.RemoveEmptyEntries))
            using (var hunspell = new Hunspell("en_US.aff", "en_US.dic"))
            {
                return input.SelectMany(line => Regex.Split(line, @"[^\p{L}]*\p{Z}[^\p{L}]*"))
                    .Select(x =>
                    {
                        var word = x.ToLower();
                        //var morphs = hunspell.Analyze(word);
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
                Console.WriteLine("");
                Console.WriteLine("Find the word stem of the word 'decompressed'");
                List<string> stems = hunspell.Analyze("decompressed");
                foreach (string stem in stems)
                {
                    Console.WriteLine("Word Stem is: " + stem);
                }
            }
        }
    }
}