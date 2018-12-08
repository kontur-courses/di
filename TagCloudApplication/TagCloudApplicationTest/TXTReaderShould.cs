using System;
using System.IO;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TagCloudApplication.Readers;

namespace TagCloudApplicationTest
{
    [TestFixture]
    public class TXTReaderShould
    {
        private TXTReader testTXTReader;

        [SetUp]
        public void SetUp()
        {
            testTXTReader = new TXTReader();
        }

        [Test]
        public void GetText_ThrowException_WhenBadFileName()
        {
            Action testAct = () => testTXTReader.GetText("-=-=");
            testAct.Should().Throw<FileNotFoundException>();
        }

        [Test]
        public void GetText_ReturnFileText_ThenFileIsExist()
        {
            var filename = AppContext.BaseDirectory + "test.txt";
            var fileText = $"Hello!{Environment.NewLine}World{Environment.NewLine}";
            using (var fs = new FileStream(filename, FileMode.Create))
            {
                var data = Encoding.Default.GetBytes(fileText);
                fs.Write(data, 0, data.Length);
            }

            testTXTReader.GetText(filename).Should().Be(fileText);
        }

        [Test]
        public void GetText_ReturnFileText_WithCustomEncoding()
        {
            var filename = AppContext.BaseDirectory + "testEncoding.txt";
            var fileText = $"Hello!{Environment.NewLine}World{Environment.NewLine}";
            var customEncoding = Encoding.BigEndianUnicode;
            using (var fs = new FileStream(filename, FileMode.Create))
            {
                var data = customEncoding.GetBytes(fileText);
                fs.Write(data, 0, data.Length);
            }

            testTXTReader.GetTextWithEncoding(filename, customEncoding).Should().Be(fileText);
        }


    }
}
