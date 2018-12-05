using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudContainer.Processing;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class PartOfSpeechDetectorShould
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (dir != null)
            {
                Environment.CurrentDirectory = dir;
                Directory.SetCurrentDirectory(dir);
            }
            else
                throw new NullReferenceException("Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) returns null");
        }

        [TestCase(null, Description = "Null word")]
        [TestCase("", Description = "Empty word")]
        public void ThrowExceptionOnInvalidWords(string word)
        {
            Action action = () => PartOfSpeechDetector.Detect(word);

            action.Should().Throw<ArgumentException>().WithMessage("Слово не должно быть пустым или null");
        }


        [TestCase("замечательный")]
        [TestCase("благоустроенный")]
        public void DetectAdjective(string word)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            PartOfSpeechDetector.Detect(word).Should().Be(PartOfSpeech.Adjective);
        }

        [TestCase("весело")]
        [TestCase("по-приятельски")]
        public void DetectAdverb(string word)
        {
            PartOfSpeechDetector.Detect(word).Should().Be(PartOfSpeech.Adverb);
        }

        [TestCase("конституций")]
        [TestCase("работа")]
        public void DetectNoun(string word)
        {
            PartOfSpeechDetector.Detect(word).Should().Be(PartOfSpeech.Noun);
        }

        [TestCase("убежать")]
        [TestCase("благоухал")]
        public void DetectVerb(string word)
        {
            PartOfSpeechDetector.Detect(word).Should().Be(PartOfSpeech.Verb);
        }
    }
}
