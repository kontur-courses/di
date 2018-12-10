using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudVisualization.WordsFileReading;


namespace TagsCloudVisualizationTest
{
    [TestFixture]
    public class TextFileReaderTest
    {
        [Test]
        public void Should_ReadFile_Correctly()
        {
            var pathToWords = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "Resources", "words.txt");
            var reader = new TextFileReader();

            var words = reader.ReadAllWords(pathToWords, "txt");
            words.Should().BeEquivalentTo(new List<string> {"машина", "человек", "робот"});
        }
    }
}
