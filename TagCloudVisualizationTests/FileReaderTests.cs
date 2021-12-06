using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagCloudVisualizationTests
{
    [TestFixture]
    public class FileReaderTests
    {
        private readonly FileReader fileReader = new();

        [Test]
        public void FileReader_ShouldReadCorrectly_WhenOneWordOnLine()
        {
            const string filePath = "FileWithOneWordOnLine.txt";

            var writer = new StreamWriter(filePath);
            writer.WriteLine("One");
            writer.WriteLine("Word");
            writer.WriteLine("On");
            writer.WriteLine("Line");
            writer.Close();

            var actual = fileReader.GetWordsFromFile(new StreamReader(filePath), new[] { ' ' });

            actual.Should().BeEquivalentTo("One", "Word", "On", "Line");
        }

        [Test]
        public void FileReader_ShouldReadCorrectly_WhenSeveralWordsOnLine()
        {
            const string filePath = "FileWithSeveralWordsOnLine.txt";

            var writer = new StreamWriter(filePath);
            writer.WriteLine("One Two Three");
            writer.WriteLine("Words Where");
            writer.WriteLine("On Where");
            writer.WriteLine("Line Clear");
            writer.Close();

            var actual = fileReader.GetWordsFromFile(new StreamReader(filePath), new[] { ' ' });

            actual.Should().BeEquivalentTo("One", "Two", "Three", "Words", "Where", "On", "Where", "Line", "Clear");
        }
    }
}