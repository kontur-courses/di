using System.IO;
using System.Linq;
using FluentAssertions;
using GemBox.Document;
using NUnit.Framework;
using TagCloud.Visualizer.Console;
using TextReader = TagCloud.Visualizer.Console.TextReader;

namespace TagCloud.Tests
{
    public class TextReaderTests
    {
        private static readonly string SourcePath = Path.Combine(Directory.GetCurrentDirectory(),
            "..",
            "..",
            "..",
            "test-files");

        [SetUp]
        public void SetUp()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
        
        [Test]
        public void GetWords_ExcludeBoringWords()
        {
            var words = TextReader.GetWords(new InputOptions("test", "txt"), SourcePath);

            words.Should().NotContain("это");
        }

        [Test]
        public void GetWords_ReadLinesFromTxtDocument()
        {
            var words = TextReader.GetWords(new InputOptions("test", "txt"), SourcePath)
                .ToArray();

            words.Should().Contain(new[] {"большой", "средний", "маленький"});
            words.Should().HaveCount(6);
        }

        [Test]
        public void GetWords_ReadLinesFromDocxDocument()
        {
            var words = TextReader.GetWords(new InputOptions("test", "docx"), SourcePath)
                .ToArray();
            
            words.Should().Contain(new[] {"большой", "средний", "маленький"});
            words.Should().HaveCount(6);
        }
    }
}