using FluentAssertions;
using NUnit.Framework;
using Visualization;

namespace CloudTagVisualizer.Tests
{
    [TestFixture]
    public class RussianWordsParser_Should
    {
        private readonly IWordsParser parser = new RussianWordsParser();

        [TestCase("", new string[0], TestName = "When empty string given")]
        [TestCase("привет", new [] {"привет"}, TestName = "When whole string is one word")]
        [TestCase("привет мир", new [] {"привет", "мир"}, TestName = "When words been separated by space")]
        [TestCase("привет,мир", new [] {"привет", "мир"}, TestName = "When words been separated by comma")]
        [TestCase("...привет...", new [] {"привет"}, TestName = "When word been separated from two sides")]
        public void ReturnExpected_When(string fullString, string[] expected)
        {
            var result = parser.Parse(fullString);

            result.Should().BeEquivalentTo(expected);
        }
        
    }
}