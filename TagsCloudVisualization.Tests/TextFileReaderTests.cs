using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Common.FileReaders;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class TextFileReaderTests
    {
        private TextFileReader reader;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            reader = new TextFileReader();
        }

        [Test]
        public void ReadFile_ShouldReadTextFileCorrectly()
        {
            var testFile = TestContext.CurrentContext.TestDirectory + @"\TestData\txt\Test_Облако.txt";
            const string expected = "Облако\r\nВ облаке\r\nНа облаке\r\nЗа обЛакОм\r\nОб облакЕ\r\n" +
                                    "Я облако\r\nМы облака\r\nНикаких облаков\r\nКаким-нибудь облаком\r\nНеким облаком";

            var actual = reader.ReadFile(testFile);

            actual.Should().Be(expected);
        }

        [Test]
        public void ReadLines_ShouldReadTextFileCorrectly()
        {
            var testFile = TestContext.CurrentContext.TestDirectory + @"\TestData\txt\Test_Облако.txt";
            const string expected = "Облако\r\nВ облаке\r\nНа облаке\r\nЗа обЛакОм\r\nОб облакЕ\r\n" +
                                    "Я облако\r\nМы облака\r\nНикаких облаков\r\nКаким-нибудь облаком\r\nНеким облаком";

            var actual = reader.ReadLines(testFile)
                .Aggregate((line1, line2) => line1 + Environment.NewLine + line2);

            actual.Should().Be(expected);
        }
    }
}