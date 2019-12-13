using System;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using TagsCloud.Spliters;

namespace TagsCloudTests
{
    class SpliterByLine_Should
    {
        private SpliterByLine spliterByLine;

        [SetUp]
        public void SetUp()
        {
            spliterByLine = new SpliterByLine();
        }

        [Test]
        public void SplitText_Should_SplitStringByEnviromentNewLine()
        {
            var words = new List<string>() { "съешь", "ещё", "этих", "мягких", "французских", "булок", "да", "выпей", "чаю"};
            var inputString = string.Join(Environment.NewLine, words);
            spliterByLine.SplitText(inputString).Should().BeEquivalentTo(words);
        }

        [Test]
        public void SplitText_Should_RemoveEmptyLines()
        {
            var words = new List<string>() { "съешь", "", "ещё", " ", "этих", "  ", "мягких", "\n", "французских", "булок", "да", "выпей", "чаю" };
            var wordsWithoutEmptyLines = new List<string>() { "съешь", "ещё", "этих", "мягких", "французских", "булок", "да", "выпей", "чаю" };
            var inputString = string.Join(Environment.NewLine, words);
            spliterByLine.SplitText(inputString).Should().BeEquivalentTo(wordsWithoutEmptyLines);
        }
    }
}
