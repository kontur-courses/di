using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.App.Layouter;


namespace TagsCloudContainer_Should

{
    public class SimpleTagsExtractor_Should
    {
        private SimpleTagsExtractor simpleTagsExtractor;

        [OneTimeSetUp]
        public void SetUp()
        {
            simpleTagsExtractor = new SimpleTagsExtractor();
        }

        [Test]
        public void FindAllTagsInText_ShouldReturnLowerСase()
        {
            var expectedWord = "мама";
            simpleTagsExtractor.FindAllTagsInText("Мама");
            var actual = simpleTagsExtractor.Text;

            actual.Should().ContainKey(expectedWord);
        }

        [TestCase("я")]
        [TestCase("и")]
        [TestCase("про")]
        [TestCase("ах")]
        [TestCase("не")]
        public void FindAllTagsInText_ShouldNotReturnNotBoringWords(string input)
        {
            simpleTagsExtractor.FindAllTagsInText(input);
            var actual = simpleTagsExtractor.Text;

            actual.Should().HaveCount(0);
        }

        [TestCase("маме", "мама")]
        [TestCase("зелёная", "зелёный")]
        [TestCase("читал", "читать")]
        public void FindAllTagsInText_ShouldReturnWordWithSimpleForm(string input, string expectedWord)
        {
            simpleTagsExtractor.FindAllTagsInText(input);
            var actual = simpleTagsExtractor.Text;

            actual.Should().ContainKey(expectedWord);
        }

        [Test]
        public void FindAllTagsInText_ShouldReturnOneWord_WithSameWords()
        {
            var expected = new Dictionary<string, int> { { "мама", 2 } };
            simpleTagsExtractor.FindAllTagsInText("мама\r\nмама");
            var actual = simpleTagsExtractor.Text;

            actual.Should().HaveCount(1);
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
