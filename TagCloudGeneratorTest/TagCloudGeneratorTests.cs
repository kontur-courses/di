using FluentAssertions;
using NUnit.Framework;
using TagCloudGenerator;

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

            var fileText = textProcessor.ProcessTheText(filePath);

            fileText.Should().Be("��������\r\n������\r\n�����\r\n��\r\n�����");
        }

        [Test]
        public void WhenPassBoringWords_ShouldReturnWordsWithoutBoringWords()
        {
            var filePath = @"C:\Users\lholy\Documents\GitHub\di\TagCloudGeneratorTest\TestsData\test2.txt";

            var fileText = textProcessor.ProcessTheText(filePath);

            fileText.Should().Be("");
        }
    }
}