using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.WordsProcessing;

namespace TagsCloud.Tests
{
    class WordsFrequencyParser_Should
    {
        private List<string> words = new List<string>();
        private string testFilePath = Assembly.GetExecutingAssembly().Location + "test.txt";

        [SetUp]
        public void SetUp()
        {
            var words = new List<string> {"repeat", "repeat", "repeat", "repeat", "repeat", "one", "two", "three", "four", "five"};
            this.words = words;
            var sampleText = string.Join(Environment.NewLine, words);
            File.AppendAllText(testFilePath, sampleText);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(testFilePath);
        }

        [TestCase("")]
        [TestCase("repeat")]
        public void ProvideCorrectData(params string[] toIgnore)
        {
            var parser = new WordsFrequencyParser(toIgnore);
            parser.ParseWordsFrequencyFromFile(testFilePath)
                .Should().NotContainKeys(toIgnore)
                .And.ContainKeys(words.Where(word => !toIgnore.Contains(word)));
        }
    }
}
