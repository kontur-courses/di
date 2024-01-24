using FluentAssertions;
using NUnit.Framework;
using TagCloudGenerator;
using System.IO;
using System;

namespace TagCloudGeneratorTest
{
    public class Tests
    {
        private TextProcessor textProcessor;

        [SetUp]
        public void Setup()
        {
            var processor = new TextProcessor();
            textProcessor = processor;
        }

        [Test]
        public void WhenPassWordsInUppercase_ShouldReturnWordsInLowerCase()
        {
            var filePath = @"C:\Users\lholy\Documents\GitHub\di\TagCloudGeneratorTest\TestsData\test1.txt";
            var file = File.ReadAllLines(filePath);

            var fileText = textProcessor.ProcessText(file);

            var result = "";
            for(var i = 0; i < fileText.Length; i++)
            {
                if (i == fileText.Length - 1)
                {
                    result += fileText[i];
                    continue;
                }
                result += (fileText[i] + Environment.NewLine);
            }

            result.Should().Be("создание\r\nоблака\r\nтегов\r\nиз\r\nфайла");
        }
    }
}