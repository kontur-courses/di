using NUnit.Framework;
using FluentAssertions;
using TagCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TagCloudTests
{
    [TestFixture]
    public class DefaultExtractorTests
    {
        private DefaultExtractor defaultExtractor;

        [SetUp]
        public void BaseSetUp()
        {
            defaultExtractor = new DefaultExtractor();
        }

        [Test]
        public void ExtractWordTokenShould_ThrowArgumentNullException_OnNullText()
        {
            Action action = () => defaultExtractor.ExtractWords(null);
            action.Should().Throw<ArgumentNullException>();
        }

        [TestCase("", ExpectedResult = 0)]
        [TestCase("\r\n", ExpectedResult = 0)]
        [TestCase("\r\n\r\n\r\n", ExpectedResult = 0)]
        [TestCase("Foo", ExpectedResult = 1)]
        [TestCase("Foo\r\n", ExpectedResult = 1)]
        [TestCase("Foo\r\nFoo", ExpectedResult = 2)]
        public int ExtractWordTokenShould_ReturnCorrectNumberOfWords(string text)
        {
            return defaultExtractor.ExtractWords(text).Length;
        }
    }
}
