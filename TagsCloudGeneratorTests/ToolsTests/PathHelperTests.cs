using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudGenerator.Tools;

namespace TagsCloudGeneratorTests.ToolsTests
{
    public class PathHelperTests
    {
        [Test]
        public void GetFileExtension_InvalidPath_ShouldThrowArgumentException()
        {
            var path = "texttexttext";

            Action act = () => PathHelper.GetFileExtension(path);

            act.Should().Throw<ArgumentException>();
        }


        [TestCase("words.txt", "txt")]
        [TestCase("file.xml", "xml")]
        [TestCase("something.doc", "doc")]
        [TestCase("something.docx", "docx")]
        [TestCase("arch.zip", "zip")]
        public void GetFileExtension_ValidFileName_ShouldReturnRightValue(string path, string expected)
        {
            var actual = PathHelper.GetFileExtension(path);
            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase(@"..\q\2\words.txt", "txt")]
        [TestCase(@"4\2\..\file.xml", "xml")]
        [TestCase(@"..\..\..\some\thing.doc", "doc")]
        [TestCase(@"..\4\something\2.docx", "docx")]
        [TestCase(@"..\..\path\arch.zip", "zip")]
        public void GetFileExtension_ValidPath_ShouldReturnRightValue(string path, string expected)
        {
            var actual = PathHelper.GetFileExtension(path);
            actual.Should().BeEquivalentTo(expected);
        }
    }
}