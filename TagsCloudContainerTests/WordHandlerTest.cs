using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudContainer;
using TagsCloudContainer.TextReaders;
using TagsCloudContainer.WorkWithWords;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class WordHandlerTest
    {
        private TextReaderGenerator _generator;
        private Settings _settings;
        private string _projectDirectory;

        [SetUp]
        public void SetUp()
        {
            _generator = new TextReaderGenerator();
            _settings = new Settings()
            {
                WordFontName = "Arial",
                WordFontSize = 16
            };
            _projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        }


        [TestCase("OneCharacters.txt")]
        [TestCase("OneCharacters.docx")]
        public void ProcessWords_OneCharacters_ShouldReturnEmptyArray(string fileName)
        {
            _settings.FileName = $"{_projectDirectory}\\TextFiles\\{fileName}";
            var fileText = GetText(_settings.FileName);
            var handler = new WordHandler(_settings);
            ;
            var words = handler.ProcessWords(fileText);
            words.Should().BeEmpty();
        }

        [TestCase("AllWordsAreBoring.txt", "BoringWords.docx")]
        [TestCase("AllWordsAreBoring.docx", "BoringWords.docx")]
        public void ProcessWords_BoringWords_ShouldReturnEmptyArray(string fileName, string boringFileName = "")
        {
            _settings.FileName = $"{_projectDirectory}\\TextFiles\\{fileName}";
            _settings.BoringWordsFileName = $"{_projectDirectory}\\TextFiles\\{boringFileName}";

            var fileText = GetText(_settings.FileName);
            var boringText = GetText(_settings.BoringWordsFileName);
            var handler = new WordHandler(_settings);
            ;
            var words = handler.ProcessWords(fileText, boringText);
            words.Should().BeEmpty();
        }

        [TestCase("UpperCase.txt")]
        [TestCase("UpperCase.docx")]
        public void ProcessWords_ShouldReturnWordsInLowerCase(string fileName)
        {
            _settings.FileName = $"{_projectDirectory}\\TextFiles\\{fileName}";
            var fileText = GetText(_settings.FileName);
            var handler = new WordHandler(_settings);
            var words = handler.ProcessWords(fileText);
            for (int i = 0; i < words.Count; i++)
                words[i].Value.Should().Be(words[i].Value.ToLower());
        }

        [TestCase("UpperCase.txt")]
        [TestCase("UpperCase.docx")]
        public void ProcessWords_ShouldReturnCorrectWords(string fileName)
        {
            var correctWords = new List<Word>()
            {
                new Word("just")
                {
                    Count = 5
                },
                new Word("simple")
                {
                    Count = 1
                },
                new Word("words")
                {
                    Count = 3
                },
                new Word("file")
                {
                    Count = 1
                }
            };
            _settings.FileName = $"{_projectDirectory}\\TextFiles\\{fileName}";
            var fileText = GetText(_settings.FileName);
            var handler = new WordHandler(_settings);
            var words = handler.ProcessWords(fileText);
            for (int i = 0; i < words.Count; i++)
                words[i].Count.Should().Be(correctWords[i].Count);
        }

        private string GetText(string pathToFile)
        {
            var reader = _generator.GetReader(pathToFile);
            return reader.GetTextFromFile(pathToFile);
        }
    }
}