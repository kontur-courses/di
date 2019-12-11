using System.Collections.Generic;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.TextParser;
using TagCloud.TextProvider;

namespace TagCloud_Should
{
    public class TextParser_Should
    {
        private TextParser textParser;
        private UnitTestsTextProvider textProvider;

        [SetUp]
        public void SetUp()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UnitTestsTextProvider>().As<ITextProvider, UnitTestsTextProvider>();
            builder.RegisterType<TextParser>().AsSelf();
            var container = builder.Build();
            textParser = container.Resolve<TextParser>();
            textProvider = container.Resolve<UnitTestsTextProvider>();
        }

        [Test]
        public void TextParser_ShouldParseComplicatedLines()
        {
            textParser.ParseText().Should().BeEquivalentTo(new List<string>
            {
                "wOrd1", "Word2", "word", "tHan", "more", "word", "word1", "word", "WORD1", "word1", "word1", "word2",
                "WORD2", "wORd2", "blacklistword", "blacklistword", "word3", "blacklistword", "word", "the", "worD",
                "am", "I", "wOrd", "Is", "It", "UnIt", "TesT", "are", "u", "mad", "a", "b", "c", "D", "e", "f", "g",
                "h", "i", "j", "k", "l", "m", "n"
            });
        }

        [Test]
        public void ShouldParseLinesWithSpaces()
        {
            textParser.ParseAllLines(textProvider.GetLineWithSpaces()).Should().BeEquivalentTo(new List<string>
            {
                "sentence", "with", "spaces", "must", "be", "parsed", "correctly"
            });
        }

        [Test]
        public void ShouldParseLinesWithPunctuationSigns()
        {
            textParser.ParseAllLines(textProvider.GetLineWithPunctuationSigns()).Should().BeEquivalentTo(
                new List<string>
                {
                    "sentence", "with", "punctuation", "signs", "must", "be", "parsed", "correctly", "Isn", "t", "it"
                });
        }
    }
}