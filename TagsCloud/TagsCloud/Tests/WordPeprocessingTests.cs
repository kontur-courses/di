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
        private List<string> _getter = new List<string>() { "жук","жуку","жука","жуки","жужжит","жужжал","жужжать"};

        [Test]
        public void ProcessWords_ReturnsInfinitiveForm_OnInfParameter()
        {
            var cleaner = new WordsCleaner(true);
            
            var words = cleaner.ProcessWords(_getter);
            
            _statisticGetter.GetWordsStatistics(words).Should().HaveCount(2)
                .And.Contain(new KeyValuePair<string, int>("жужжать", 3))
                .And.Contain(new KeyValuePair<string, int>("жук", 4));
        }
        [Test]
        public void ProcessWords_IgnoreBoring_OnSimpleInput()
        {
            var cleaner = new WordsCleaner(true);
            _getter
                .Append("но")
                .Append("что")
                .Append("где")
                .Append("когда")
                .Append("я");
            
            var words = cleaner.ProcessWords(_getter);
            
            _statisticGetter.GetWordsStatistics(words).Should().HaveCount(2)
                .And.Contain(new KeyValuePair<string, int>("жужжать", 3))
                .And.Contain(new KeyValuePair<string, int>("жук", 4));
        }

    }
}