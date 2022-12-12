using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordProcessors;
using TagsCloudVisualization.WordProcessors.WordProcessingSettings;

namespace TagsCloudVisualization.TagsCloudVisualizationTests
{
    [TestFixture]
    internal class WordProcessorTests
    {
        [TestCase("Я Я Я Я ОченьДлинноеСловоТакоеНельзя", "", TestName = "All words of invalid length")]
        [TestCase("яблоко яблоко банан", "", TestName = "All words are excluded")]
        [TestCase("Некоторые слова разрешены, а некоторые яблоко","некоторые слова разрешены, некоторые",
            TestName = "There are forbidden words")]
        [TestCase("Все слова разрешены", "все слова разрешены",
            TestName = "All words are allowed")]
        public void Process_ShouldReturnOnlyAllowedWords(string text, string expected)
        {
            var settings = new ProcessingSettings("яблоко, банан", 3, 10);
            var processor = new WordProcessor(settings);
            processor.Process(ParseWord(text)).Should().BeEquivalentTo(ParseWord(expected));
        }

        public string[] ParseWord(string words)
        {
            return words.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
