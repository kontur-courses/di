using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.TokensGenerator;

namespace TagsCloudContainer.Tests.TokenGenerator
{
    [TestFixture]
    public class MyStemParser_Should
    {
        private MyStemParser tokenParser;
        private string word;

        [SetUp]
        public void SetUp()
        {
            word = "слово";
            tokenParser = new MyStemParser(new FileManager.FileManager());
        }

        [Test]
        public void GetTokens_WhenNull_ThrowArgumentException()
        {
            Action act = () => { tokenParser.GetTokens(null); };
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void GetTokens_WhenEmpty()
        {
            tokenParser.GetTokens("").Should().HaveCount(0);
        }

        [Test]
        public void GetTokens_WhenWord_ReturnWord()
        {
            tokenParser.GetTokens(word).First().Should().Be(word);
        }

        [Test]
        public void GetTokens_WhenOneWord_ContainOneTokenWithCountIsOne()
        {
            var token = tokenParser.GetTokens(word);
            token.Should().HaveCount(1);
        }

        [Test]
        public void GetTokens_WhenDuplicate_ContainOneToken()
        {
            var result = tokenParser.GetTokens(word + Environment.NewLine + word);
            Console.WriteLine(result);
            result.Should().HaveCount(2);
        }

        [Test]
        public void GetTokens_WhenDuplicate_TokenCountIsTwo()
        {
            var token = tokenParser.GetTokens(word + Environment.NewLine + word);
            token.Should().HaveCount(2);
        }

        [Test]
        public void GetTokens_WhenPunctuation_ShouldntContainPunctuation()
        {
            var text = "Просто - предложение, предложение просто.\r\nТочка.";
            var token = tokenParser.GetTokens(text);
            token.Should().NotContain(new []{",",".", "-"});
        }

        [Test]
        public void GetTokens_WhenWordWithCapitalLetter_ShouldContainWordWithLowerSymbol()
        {
            var text = "Слова С Большой Буквы";
            var token = tokenParser.GetTokens(text);
            token.All(el => el.ToLower() == el).Should().BeTrue();
        }
    }
}