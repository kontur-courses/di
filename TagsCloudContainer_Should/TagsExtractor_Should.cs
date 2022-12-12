using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.App.Layouter;

namespace TagsCloudContainerTests

{
    public class TagsExtractor_Should
    {
        private TagsExtractor tagsExtractor;

        [OneTimeSetUp]
        public void SetUp()
        {
            tagsExtractor = new TagsExtractor();
        }

        [Test]
        public void FindAllTagsInText_ShouldReturnLowerСase()
        {
            var expectedWord = "мама";
            var actual = tagsExtractor.FindAllTagsInText("Мама");

            actual.Should().ContainKey(expectedWord);
        }

        [TestCase("я")]
        [TestCase("и")]
        [TestCase("про")]
        [TestCase("ах")]
        [TestCase("не")]
        public void FindAllTagsInText_ShouldNotReturnNotBoringWords(string input)
        {
            var actual = tagsExtractor.FindAllTagsInText(input);

            actual.Should().HaveCount(0);
        }

        [TestCase("маме", "мама")]
        [TestCase("зелёная", "зелёный")]
        [TestCase("читал", "читать")]
        public void FindAllTagsInText_ShouldReturnWordWithSimpleForm(string input, string expectedWord)
        {
            var actual = tagsExtractor.FindAllTagsInText(input);

            actual.Should().ContainKey(expectedWord);
        }

        [Test]
        public void FindAllTagsInText_ShouldReturnOneWord_WithSameWords()
        {
            var expected = new Dictionary<string, int> { { "мама", 2 } };
            var actual = tagsExtractor.FindAllTagsInText("мама"+ Environment.NewLine+ "мама");

            actual.Should().HaveCount(1);
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
