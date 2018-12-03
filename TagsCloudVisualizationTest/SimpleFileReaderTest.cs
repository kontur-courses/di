using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using TagsCloudVisualization;
using FluentAssertions;


namespace TagsCloudVisualizationTest
{
    [TestFixture]
    public class SimpleFileReaderTest
    {
        [Test]
        public void Should_ReadFile_Correctly()
        {
            var sr = new TextFileReader(
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    "Resources", "words.txt"));
            var words = sr.ReadAllWords();
            words.Should().BeEquivalentTo(new List<string> {"машина", "человек", "робот"});
        }
    }
}
