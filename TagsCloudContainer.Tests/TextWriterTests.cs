using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TextWriter = TagsCloudContainer.TextWriter;

namespace TagsCloudVisualization.Tests
{
    public class TextWriterTests
    {
        private TextWriter Writer { get; set; }
        private string Root { get; set; }

        [SetUp]
        public void SetUp()
        {
            Writer = new TextWriter();
            Root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        }

        [TestCase(@"<html></html>", TestName = "Directory dont exist")]
        [TestCase(@"C:_Dir_text.txt", TestName = "Not backslash separator")]
        [TestCase(@"C:\text txt", TestName = "Doesnt have dot separator")]
        [TestCase(@"C:\text.", TestName = "Doesnt have filename extension")]
        public void ThrowException_When(string path)
        {
            Action writeText = () => Writer.WriteText("text", path);

            writeText.Should().Throw<ArgumentException>();
        }

        [Test]
        public void DoesntThrowException_When()
        {
            var path = $"C:{Path.DirectorySeparatorChar}image.png";
            Action writeText = () => Writer.WriteText("text", path);

            writeText.Should().NotThrow<ArgumentException>();
            File.Delete(path);
        }

        [TestCaseSource(nameof(TestCases))]
        public void SaveFileInRightFormat_When(string text, string expectedResult)
        {
            var path = Path.Join(Root, "TagsCloudContainer", "Texts", "test.txt");
            Writer.WriteText(text, path);

            File.Exists(path).Should().BeTrue();
            File.ReadAllText(path).Should().Be(expectedResult);
            File.Delete(path);
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData("one two three", $"one{Environment.NewLine}two{Environment.NewLine}three")
                .SetName("Just words");
            yield return new TestCaseData("", "").SetName("Empty string");
            yield return new TestCaseData("Dot, net.", $"Dot{Environment.NewLine}net").SetName("With delimiters");
            yield return new TestCaseData("12 34", $"12{Environment.NewLine}34").SetName("Digits");
            yield return new TestCaseData($"New. {Environment.NewLine} line.", $"New{Environment.NewLine}line")
                .SetName("New line");
        }
    }
}