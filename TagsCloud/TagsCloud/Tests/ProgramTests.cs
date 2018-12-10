using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.TagsCloudVisualization;

namespace TagsCloud.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        private string filePath =
            Path.Combine(Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory))), 
                "Tests","TestFile.txt");

        private static List<Tag> GetTags(string[] args)
        {
            var options = Options.Parse(args);
            var container = Program.BuildContainer(options);
            return Program.GetTags(options, container);
        }

        [Test]
        public void GetTags_ThrowException_OnWrongArgs()
        {
            Action act = () => GetTags(new []{ "--something" });
            act.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void GetTags_ReturnCorrectAmountOfTags_OnSimpleOptions()
        {
            var tags = GetTags(new[] { "--file", filePath});
            tags.Count.Should().Be(6);
        }

        [Test]
        public void GetTags_ReturnCorrectAmountOfTags_OnChangedBoringPartsOfSpeech()
        {
            var tags = GetTags(new[] { "--file", filePath, "--boring", "Verb", "Noun"});
            tags.Count.Should().Be(1);
        }

        [Test]
        public void GetTags_ReturnTagsWithCorrectFontSize()
        {
            var tags = GetTags(new[] { "--file", filePath});
            foreach (var tag1 in tags)
            {
                foreach (var tag2 in tags)
                    if (tag1.Frequency > tag2.Frequency)
                        tag1.FontSize.Should().BeGreaterThan(tag2.FontSize);
            }
        }

        [Test]
        public void GetTags_ReturnCorrectAnountOfTags_OnInfinitiveOption()
        {
            var tags = GetTags(new[] { "--file", filePath, "--infinitive"});
            tags.Count.Should().Be(3);
        }

        [Test]
        public void GetTags_ReturnCorrectAnountOfTags_OnDefinedPartsOfSpeech()
        {
            var tags = GetTags(new[] { "--file", filePath, "--only", "Noun" });
            tags.Count.Should().Be(4);
        }

        [Test]
        public void GetTags_ReturnsTagsWithLoweredWords()
        {
            var tags = GetTags(new[] { "--file", filePath});
            foreach (var tag in tags)
            {
                (tag.Word.ToLower() == tag.Word).Should().BeTrue();
            }
        }
    }
}
