using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Input;

namespace TagsCloudContainerTests.Input
{
    [TestFixture]
    public class WordParserShould
    {
        [Test]
        public void ParseTxt()
        {
            new TxtParser().ParseWords("hello  world ha ha").Should().BeEquivalentTo("hello", "world", "ha", "ha");
        }
    }
}