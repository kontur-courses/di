using FluentAssertions;
using NUnit.Framework;
using TagCloudGenerator;
using System;

namespace TagCloudGeneratorTest
{
    public class Tests
    {
        private TextProcessor textProcessor;
        private WordCounter counter;

        [SetUp]
        public void Setup()
        {
            textProcessor = new TextProcessor();
            counter = new WordCounter();
        }

        [Test]
        public void WhenPassWordsInUppercase_ShouldReturnWordsInLowerCase()
        {
            var text = textProcessor.ProcessText(new[] { "Облако", "Тегов" });

            var result = "";
            foreach (var word in text)
                result += (word + Environment.NewLine);
            
            result.Should().Be("облако\r\nтегов\r\n");
        }

        [Test]
        public void WhenWordIsRepeatedSeveralTimesInText_ItShouldBeOutputOneTime()
        {
            var text = textProcessor.ProcessText(new[] { "Облако", "Тегов", "Облако"});
            var dictionary = counter.CountWords(text);

            dictionary["облако"].Should().Be(2);
        }
    }
}