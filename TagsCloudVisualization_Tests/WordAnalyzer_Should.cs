using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualization_Tests
{
    class WordAnalyzer_Should
    {
        private WordAnalyzer analyzer;
        [SetUp]
        public void SetUp()
        {
            analyzer = new WordAnalyzer();
        }

        [Test]
        public void RemoveSpecialSymbols_TakesStringWithSpecialSymbols_ReturnStringOnlyWithChars()
        {
            analyzer.RemoveSpecialSymbols("hello~!@#$%^&*()+|{}:<>world?№;%:?*/[]';-=").Should().Be("hello world ");
        }

        [Test]
        public void TextAnalyzer_GetSomeText_ReturnCorrectCountOfWords()
        {
            var text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent convallis convallis quam, vitae lacinia massa. Donec quis eleifend lorem, et dapibus orci. Aliquam rutrum est ac ex tempor, at molestie ante scelerisque. Donec feugiat quis erat dapibus pharetra. Ut vestibulum velit quis libero efficitur, ut elementum ipsum ultricies. Proin eu dolor risus. Vivamus vehicula facilisis orci, in pulvinar quam dignissim ullamcorper. Duis varius gravida sapien, volutpat congue ante volutpat vitae. Nam eget laoreet nulla. Sed porttitor eros vitae tellus vehicula, non iaculis purus vestibulum. Phasellus aliquet quam et neque condimentum congue. Mauris in sem vel nulla placerat varius id sed arcu.";
            analyzer.TextAnalyzer(text).Count.Should().Be(101);
        }

        [Test]
        public void WeightWords_GetSomeText_CorrectFrequencies()
        {
            var words = analyzer.TextAnalyzer("hello world hello hello world why?");
            var weightedWords = new Dictionary<String, int>();
            weightedWords.Add("hello", 3);
            weightedWords.Add("world", 2);
            weightedWords.Add("why", 1);
            analyzer.WeightWords(words).ShouldBeEquivalentTo(weightedWords);
        }

    }
}
