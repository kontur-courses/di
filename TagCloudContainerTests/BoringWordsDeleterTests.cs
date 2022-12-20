using System;
using CommandLine;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudContainer.UI;

namespace TagCloudContainerTests
{
    public class BoringWordsDeleterTests
    {
        private IUi parsedArguments;
        [SetUp]
        public void SetUp()
        {
            var app = new ConsoleUiSettings();
           parsedArguments = Parser.Default.ParseArguments<ConsoleUiSettings>(new string[] { }).Value;
        }
        [Test]
        public void BoringWordsDeleter_ShouldNotDeleteNotBoringWords()
        {
            parsedArguments.ExceptPartOfSpeech = "";
            var notBoringWords = new[] {"котик", "котенок", "кошка", "кисуля", "котяра"};
            var result = BoringWordsDeleter.DeleteBoringWords(notBoringWords, parsedArguments);
            result.Should().BeEquivalentTo(notBoringWords);
        }

        [Test]
        public void BoringWordsDeleter_ShouldDeleteBoringWords()
        {
            parsedArguments.ExceptPartOfSpeech = "";
            var notBoringWords = new[] {"кто", "на", "где", "а", "и"};
            var result = BoringWordsDeleter.DeleteBoringWords(notBoringWords, parsedArguments);
            result.Should().BeEmpty();
        }

        [Test]
        public void BoringWordsDeleter_ShouldDeleteEmptyStrings()
        {
            var notBoringWords = new[] {"а", "и", "не", "я", "спатеньки"};
            var result = BoringWordsDeleter.DeleteBoringWords(notBoringWords, parsedArguments);
            result.Should().BeEquivalentTo("спатеньки");
        }
    }
}