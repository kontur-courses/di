using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;
using TagsCloudVisualization.WordProcessors;

namespace TagsCloudVisualizationTest
{
    [TestFixture]
    public class WordsStatisticsTests
    {
        private IContainer container;
        private IWordsStatistics wordsStatistics;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<BaseWordsStatistics>().As<IWordsStatistics>();
            builder.Register(_ =>
            {
                var processor = A.Fake<ITextProcessor>();
                A.CallTo(() => processor.ProcessWords(A<IEnumerable<string>>.Ignored)).ReturnsLazily((IEnumerable<string> text) => text);
                return processor;
            }).As<ITextProcessor>();
            container = builder.Build();
        }
        
        [SetUp]
        public void SetUp()
        {
            wordsStatistics = container.Resolve<IWordsStatistics>();
        }
        
        [Test]
        public void GetStatistics_IsEmpty_AfterCreation()
        {
            wordsStatistics.GetStatistics().Should().BeEmpty();
        }

        [Test]
        public void GetStatistics_ContainsItem_AfterAddition()
        {
            wordsStatistics.AddWords(new[] { "abc" });
            wordsStatistics.GetStatistics().Should().Equal(new WordCount("abc", 1));
        }

        [Test]
        public void GetStatistics_ContainsManyItems_AfterAdditionOfDifferentWords()
        {
            wordsStatistics.AddWords(new[] { "abc", "def" });
            wordsStatistics.GetStatistics().Should().HaveCount(2);
        }

        [Test]
        public void GetStatistics_SortsWordsByFrequency()
        {
            wordsStatistics.AddWords(new[] { "aaaaaaaaaa", "bbbbbbbbbb", "bbbbbbbbbb" });
            wordsStatistics.GetStatistics().Select(t => t.Word)
                .Should().BeEquivalentTo(new[] {"bbbbbbbbbb", "aaaaaaaaaa"},
                    options => options.WithStrictOrdering());
        }

        [Test]
        public void GetStatistics_SortsWordsByAbc_WhenFrequenciesAreSame()
        {
            wordsStatistics.AddWords(new[] { "cccccccccc", "aaaaaaaaaa", "bbbbbbbbbb" });
            wordsStatistics.GetStatistics().Select(t => t.Word)
                .Should().ContainInOrder("aaaaaaaaaa", "bbbbbbbbbb", "cccccccccc");
        }

        [Test]
        public void GetStatistics_ReturnsSameResult_OnSecondCall()
        {
            wordsStatistics.AddWords(new[] { "abc" });
            wordsStatistics.GetStatistics().Should().Equal(new WordCount("abc", 1));
            wordsStatistics.GetStatistics().Should().Equal(new WordCount("abc", 1));
        }

        [Test]
        public void AddWord_AllowsShortWords()
        {
            wordsStatistics.AddWords(new[] { "aaa" });
        }

        [Test]
        public void AddWord_CountsOnce_WhenSameWord()
        {
            wordsStatistics.AddWords(new[] { "aaaaaaaaaa", "aaaaaaaaaa" });
            wordsStatistics.GetStatistics().Should().HaveCount(1);
        }

        [Test]
        public void AddWord_IncrementsCounter_WhenSameWord()
        {
            wordsStatistics.AddWords(new[] { "aaaaaaaaaa", "aaaaaaaaaa" });
            wordsStatistics.GetStatistics().Select(t => t.Count).First().Should().Be(2);
        }

        [Test]
        public void AddWord_Ignores_EmptyWord()
        {
            wordsStatistics.AddWords(new[] { "" });
            wordsStatistics.GetStatistics().Should().BeEmpty();
        }

        [Test]
        public void AddWord_Ignores_WhitespaceWord()
        {
            wordsStatistics.AddWords(new[] { " " });
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
            for (var c = 'а'; c is <= 'я' or <= 'ё'; c++)
            {
                words.Add(c.ToString());
                words.Add(c.ToString().ToUpper());
                counter++;
            }

            wordsStatistics.AddWords(words);
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
            
            wordsStatistics.AddWords(words);
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
            
            wordsStatistics.AddWords(words);
            wordsStatistics.GetStatistics();
        }
    }
}