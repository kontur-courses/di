using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudGenerator.Tools;

namespace TagsCloudGeneratorTests.ToolsTests
{
    public class HelperTests
    {
        [Test]
        public void GetWordToCount_ArgumentIsNull_ShouldThrowArgumentNullException()
        {
            List<string> words = null;

            Action act = () => Helper.GetWordToCount(words);

            act.Should().Throw<ArgumentNullException>();
        }

        [TestCase(5)]
        [TestCase(10)]
        [TestCase(31)]
        public void GetWordToCount_NWordsToMatch_ShouldReturnDictionaryContainsThisWordAndNCount(int count)
        {
            var word = "text";
            var words = new List<string> {"doctor", "tea", "full", word, "rabbit"};

            for (var i = 0; i < count - 1; i++)
            {
                words.Add(word);
            }

            var wordToCount = Helper.GetWordToCount(words);
            var actual = wordToCount[word];

            actual.Should().Be(count);
        }

        [Test]
        public void GetWordToCount_AllWordsDifferent_ShouldReturnDictionaryContainsThisWordsWithCount1()
        {
            var words = new List<string> {"doctor", "doctors", "tea", "rabbit",};
            var expected = new Dictionary<string, int> {["doctor"] = 1, ["doctors"] = 1, ["tea"] = 1, ["rabbit"] = 1};

            var actual = Helper.GetWordToCount(words);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetFileExtension_InvalidPath_ShouldThrowArgumentException()
        {
            var path = "texttexttext";

            Action act = () => Helper.GetFileExtension(path);

            act.Should().Throw<ArgumentException>();
        }

        
        [TestCase("words.txt", "txt")]
        [TestCase("file.xml", "xml")]
        [TestCase("something.doc", "doc")]
        [TestCase("something.docx", "docx")]
        [TestCase("arch.zip", "zip")]
        public void GetFileExtension_ValidFileName_ShouldReturnRightValue(string path, string expected)
        {
            var actual = Helper.GetFileExtension(path);
            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase(@"..\q\2\words.txt", "txt")]
        [TestCase(@"4\2\..\file.xml", "xml")]
        [TestCase(@"..\..\..\some\thing.doc", "doc")]
        [TestCase(@"..\4\something\2.docx", "docx")]
        [TestCase(@"..\..\path\arch.zip", "zip")]
        public void GetFileExtension_ValidPath_ShouldReturnRightValue(string path, string expected)
        {
            var actual = Helper.GetFileExtension(path);
            actual.Should().BeEquivalentTo(expected);
        }
    }
}