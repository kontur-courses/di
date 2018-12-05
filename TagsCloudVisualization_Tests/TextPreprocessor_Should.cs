using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualization_Tests
{
    [TestFixture]
    public class TextPreprocessor_Should
    {
        private string[] boringWords;
        private string wordsToProcess;
        private TextPreprocessor preprocessor;

        [SetUp]
        public void SetUp()
        {
            boringWords = new[] {"a", "b"};
            wordsToProcess = string.Join(Environment.NewLine, new[] {"aa","aa","a", "b", "b", "gGg"});
            preprocessor = new TextPreprocessor(boringWords);
        }

        [Test]
        public void ExcludeBoringWords()
        {
            preprocessor.PreprocessWords(wordsToProcess).Select(s => s.Item1).Should().NotContain(boringWords);
        }
        
        [Test]
        public void NotRepeatWords()
        {
            preprocessor.PreprocessWords(wordsToProcess).Select(s => s.Item1).Where(s => s == "aa").Should()
                .HaveCount(1);
        }
        
        [Test]
        public void BringToLowerCase()
        {
            preprocessor.PreprocessWords(wordsToProcess).Select(s => s.Item1).Should().Contain("ggg");
        }
        
        [Test]
        public void CorrectlyCount()
        {
            preprocessor.PreprocessWords(wordsToProcess).FirstOrDefault(s => s.Item1 == "aa").Item2.Should().Be(2);
        }
        
        [Test]
        public void OrderByDescending()
        {
            preprocessor.PreprocessWords(wordsToProcess).Select(s => s.Item1).Should().ContainInOrder("aa", "ggg");
        }

    }
}