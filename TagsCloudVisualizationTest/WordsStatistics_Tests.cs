using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTest
{
    [TestFixture]
    public class WordsStatistics_Tests
    {
        [Test]
        public void GetStatistics_IsEmpty_AfterCreation()
        {
            var wordsStatistics = new WordsStatistics(new MockWordReader());
            wordsStatistics.Load();
            wordsStatistics.GetStatistics().Should().BeEmpty();
        }

        [Test]
        public void GetStatistics_ContainsItem_AfterAddition()
        {
            var wordsStatistics = new WordsStatistics(new MockWordReader("abc"));
            wordsStatistics.Load();
            wordsStatistics.GetStatistics().Should().Equal(new WordCount("abc", 1));
        }

        [Test]
        public void GetStatistics_ContainsManyItems_AfterAdditionOfDifferentWords()
        {
            var wordsStatistics = new WordsStatistics(new MockWordReader("abc", "def"));
            wordsStatistics.Load();
            wordsStatistics.GetStatistics().Should().HaveCount(2);
        }

        [Test]
        public void GetStatistics_SortsWordsByFrequency()
        {
            var wordsStatistics = new WordsStatistics(new MockWordReader("aaaaaaaaaa", "bbbbbbbbbb", "bbbbbbbbbb"));
            wordsStatistics.Load();
            wordsStatistics.GetStatistics().Select(t => t.Word)
                .Should().BeEquivalentTo(new[] {"bbbbbbbbbb", "aaaaaaaaaa"},
                    options => options.WithStrictOrdering());
        }

        [Test]
        public void GetStatistics_SortsWordsByAbc_WhenFrequenciesAreSame()
        {
            var wordsStatistics = new WordsStatistics(new MockWordReader("cccccccccc", "aaaaaaaaaa", "bbbbbbbbbb"));
            wordsStatistics.Load();
            wordsStatistics.GetStatistics().Select(t => t.Word)
                .Should().ContainInOrder("aaaaaaaaaa", "bbbbbbbbbb", "cccccccccc");
        }

        [Test]
        public void GetStatistics_ReturnsSameResult_OnSecondCall()
        {
            var wordsStatistics = new WordsStatistics(new MockWordReader("abc"));
            wordsStatistics.Load();
            wordsStatistics.GetStatistics().Should().Equal(new WordCount("abc", 1));
            wordsStatistics.GetStatistics().Should().Equal(new WordCount("abc", 1));
        }

        [Test]
        public void GetStatistics_BuildsResult_OnEveryCall()
        {
            var wordsStatistics = new WordsStatistics(new MockWordReader("abc"));
            wordsStatistics.Load();
            wordsStatistics.GetStatistics().Should().HaveCount(1);
            wordsStatistics.AddWord("def");
            wordsStatistics.GetStatistics().Should().HaveCount(2);
        }

        [Test]
        public void AddWord_AllowsShortWords()
        {
            var wordsStatistics = new WordsStatistics(new MockWordReader("aaa"));
            wordsStatistics.Load();
        }

        [Test]
        public void AddWord_CountsOnce_WhenSameWord()
        {
            var wordsStatistics = new WordsStatistics(new MockWordReader("aaaaaaaaaa", "aaaaaaaaaa"));
            wordsStatistics.Load();
            wordsStatistics.GetStatistics().Should().HaveCount(1);
        }

        [Test]
        public void AddWord_IncrementsCounter_WhenSameWord()
        {
            var wordsStatistics = new WordsStatistics(new MockWordReader("aaaaaaaaaa", "aaaaaaaaaa"));
            wordsStatistics.Load();
            wordsStatistics.GetStatistics().Select(t => t.Count).First()
                .Should().Be(2);
        }

        [Test]
        public void AddWordThrows_WhenWordIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new WordsStatistics(new MockWordReader(null)));
        }

        [Test]
        public void AddWord_Ignores_EmptyWord()
        {
            var wordsStatistics = new WordsStatistics(new MockWordReader(""));
            wordsStatistics.Load();
            wordsStatistics.GetStatistics().Should().BeEmpty();
        }

        [Test]
        public void AddWord_Ignores_WhitespaceWord()
        {
            var wordsStatistics = new WordsStatistics(new MockWordReader(" "));
            wordsStatistics.Load();
            wordsStatistics.GetStatistics().Should().BeEmpty();
        }

        [Test]
        public void AddWord_IsCaseInsensitive()
        {
            var counter = 0;
            var words = new List<string>();
            for (var c = 'a'; c <= 'z'; c++)
            {
                words.Add(c.ToString());
                words.Add(c.ToString().ToUpper());
                counter++;
            }
            for (var c = 'а'; c <= 'я' || c <= 'ё'; c++)
            {
                words.Add(c.ToString());
                words.Add(c.ToString().ToUpper());
                counter++;
            }

            var wordsStatistics = new WordsStatistics(new MockWordReader(words.ToArray()));
            wordsStatistics.Load();
            wordsStatistics.GetStatistics().Should().HaveCount(counter);
        }

        [Test]
        public void AddWord_HasNoCollisions()
        {
            const int wordCount = 500;
            var words = new List<string>();
            for (var i = 0; i < wordCount; i++)
            {
                words.Add(i.ToString());
            }
            
            var wordsStatistics = new WordsStatistics(new MockWordReader(words.ToArray()));
            wordsStatistics.Load();
            wordsStatistics.GetStatistics().Should().HaveCount(wordCount);
        }

        [Test, Timeout(1500)]
        public void AddWord_HasSufficientPerformance_OnAddingDifferentWords()
        {
            var words = new List<string>();
            for (var i = 0; i < 5000; i++)
            {
                words.Add(i.ToString());
            }
            
            var wordsStatistics = new WordsStatistics(new MockWordReader(words.ToArray()));
            wordsStatistics.Load();
            wordsStatistics.GetStatistics();
        }
    }
}