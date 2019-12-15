using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    public class DefaultClassesTests
    {
        private DefaultWordsFilter wordsFilter;
        private DefaultWordsCounter wordsCounter;
        private DefaultWordsToSizesConverter wordsToSizesConverter;
        
        [SetUp]
        public void SetUp()
        {
            wordsFilter = new DefaultWordsFilter(new[] {"SPRO"});
            wordsCounter = new DefaultWordsCounter();
            wordsToSizesConverter = new DefaultWordsToSizesConverter(new Size(5000,5000));
        }
        
        [TestCase("крысавчик", "красавчик")]
        [TestCase("оно", "она")]
        public void WordsToSizesConverterShouldConvertCorrect(string arg1, string arg2)
        {
            var words = $"{arg1}\n{arg2}\n{arg1}".Split('\n');
            var count = wordsCounter.CountWords(words);
            var result = wordsToSizesConverter.GetSizesOf(count);
            Size sizeBigger = new Size();
            Size sizeLesser = new Size();
            foreach (var res in result)
            {
                if (res.Item1.Equals(arg1))
                    sizeBigger = res.Item2;
                else sizeLesser = res.Item2;
            }

            (sizeBigger.Height > sizeLesser.Height).Should().BeTrue();
            (sizeBigger.Width > sizeLesser.Width).Should().BeTrue();
        }
        
        [Test]
        public void WordsFilterShouldExcludeCorrect()
        {
            var words = "я\nкрасавчик";
            wordsFilter.FilterWords(words.Split('\n')).Should().HaveCount(1);
        }

        [Test]
        public void WordsCounterShouldCountCorrect()
        {
            var words = "я\nкрасавчик".Split('\n');
            wordsCounter.CountWords(words).Should().BeEquivalentTo(
                new Dictionary<string, int>(){ { "я",1 }, { "красавчик", 1 } });
        }
    }
}