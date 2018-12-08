using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TagsCloudPreprocessor;

namespace TagsCloudPreprocessorTests
{
    [TestFixture]
    public class TextParser_Should
    {
        private IEnumerable<string> ParseText(string text)
        {
            return new TextParser().GetWords(text);
        }

        [TestCase("a a a", ExpectedResult = new[] { "a", "a", "a" }, TestName = "By white space")]
        [TestCase("a\na\na", ExpectedResult = new[] { "a", "a", "a" }, TestName = "By \\n")]
        public string[] SeparateText(string text)
        {
            return ParseText(text).ToArray();
        }

        [TestCase("1a 2a 3a", ExpectedResult = new[] { "a", "a", "a" }, TestName = "Numbers")]
        [TestCase("a 'a'", ExpectedResult = new[] { "a", "a" }, TestName = "Quotes")]
        public string[] Exclude(string text)
        {
            return ParseText(text).ToArray();
        }
    }
}
