using System.IO;
using NUnit.Framework;
using TagCloud.TextConverters;

namespace TagCloud.Tests
{
    [TestFixture]
    class TextReaderTest
    {
        private readonly TextReaderTxt reader = new TextReaderTxt();
        private readonly string path = $".{Path.PathSeparator}fileTest.txt";

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("as dsa")]
        [TestCase("hjkl\nasdf\n\nsadf asdf fda")]
        public void WriteTextAndRead_ShouldBeEquals(string text)
        {
            File.WriteAllText(path, text);
            var result = reader.ReadText(path);
            Assert.AreEqual(text, result);
            File.Delete(path);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("as dsa")]
        [TestCase("hjkl\nasdf\n\nsadf asdf fda")]
        public void ReadTextFromFileDoesntExist_ShouldBeNull(string text)
        {
            var result = reader.ReadText(path);
            Assert.IsNull(result);
        }
    }
}
