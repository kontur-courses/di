using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.WordPreprocessing;

namespace TagsCloud.Tests
{
    [TestFixture]
    public class WordPeprocessingTests
    {
        private IWordAnalyzer _statisticGetter = new WordStatisticGetter();

        private List<string> _getter;

        [SetUp]
        public void SetUp()
        {
            _getter = new List<string>() {"жук", "жуку", "жука", "жуки", "жужжит", "жужжал", "жужжать"};
        }

        [Test]
        public void ProcessWords_ReturnsInfinitiveForm_OnInfParameter()
        {
            var cleaner = new WordsCleaner(true);

            var words = cleaner.ProcessWords(_getter);

            words
                .GroupBy(g => g)
                .ToDictionary(x => x.Key, x => x.Count())
                .Should().HaveCount(2)
                .And.Contain(new KeyValuePair<string, int>("жужжать", 3))
                .And.Contain(new KeyValuePair<string, int>("жук", 4));
        }

        [Test]
        public void ProcessWords_IgnoreBoring_OnSimpleInput()
        {
            var cleaner = new WordsCleaner(true);
            _getter.AddRange(new List<string> {"но", "что", "когда", "я", "мы"});

            var words = cleaner.ProcessWords(_getter);

            words
                .GroupBy(g => g)
                .ToDictionary(x => x.Key, x => x.Count()).Should().HaveCount(2)
                .And.Contain(new KeyValuePair<string, int>("жужжать", 3))
                .And.Contain(new KeyValuePair<string, int>("жук", 4));
        }
        
        [Test]
        public void ProcessWords_IgnoreUnknown_OnSimpleInput()
        {
            var cleaner = new WordsCleaner(true);
            _getter.AddRange(new List<string>{"dfkhhk","dfjgkskj" ,"fkjgbku","aaaafsd","szsuhhr"});

            var words = cleaner.ProcessWords(_getter);

            words
                .GroupBy(g => g)
                .ToDictionary(x => x.Key, x => x.Count()).Should().HaveCount(2)
                .And.Contain(new KeyValuePair<string, int>("жужжать", 3))
                .And.Contain(new KeyValuePair<string, int>("жук", 4));
        }
    }
}