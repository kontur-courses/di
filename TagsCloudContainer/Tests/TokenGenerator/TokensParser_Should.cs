using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Filters;
using TagsCloudContainer.TokensGenerator;

namespace TagsCloudContainer.Tests.TokenGenerator
{
    [TestFixture]
    public class TokensParser_Should
    {
        private TokensParser tokenParser;
        private string word;

        [SetUp]
        public void SetUp()
        {
            word = "aba";
            tokenParser = new TokensParser();
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

//        [Test]
//        public void GetTokens_WhenOneWord_ContainOneTokenWithCountIsOne()
//        {
//            var token = tokenParser.GetTokens(word);
//            token.Should().HaveCount(1);
//            token.First().Count.Should().Be(1);
//        }

        [Test]
        public void GetTokens_WhenDuplicate_ContainOneToken()
        {
            tokenParser.GetTokens(word + Environment.NewLine + word).Should().HaveCount(2);
        }

//        [Test]
//        public void GetTokens_WhenDuplicate_TokenCountIsTwo()
//        {
//            var token = tokenParser.GetTokens(word + Environment.NewLine + word);
//            token.Should().HaveCount(1);
//            token.First().Count.Should().Be(2);
//        }
    }
}