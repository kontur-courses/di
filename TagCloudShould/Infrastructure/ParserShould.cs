using System.Text;
using TagCloudContainer.Parsers;

namespace TagCloudShould.Infrastructure
{
    [TestFixture]
    public class ParserShould
    {
        [Test]
        public void ReturnParsedString_WhenHaveOneString()
        {
            var parser = new FileLinesParser();
            var stringForParse = "Nice day bro";
            var parsed = parser.Parse(stringForParse);
            parsed.Count().Should().Be(1);
            parsed.FirstOrDefault().Should().Be(stringForParse);
        }
        [Test]
        public void ReturnParsedString_WhenHaveManyStrings()
        {
            var parser = new FileLinesParser();
            var stringBuilder = new StringBuilder();
            var parsedString = new List<string>();
            for (var i = 0; i < 1000; i++)
            {
                parsedString.Add(i.ToString());
                stringBuilder.AppendLine(i.ToString());
            }
            var parsed = parser.Parse(stringBuilder.ToString()).ToList();
            for (var i = 1; i < 1000; i++)
                parsed[i].Should().Be(parsedString[i]);
        }
    }
}
