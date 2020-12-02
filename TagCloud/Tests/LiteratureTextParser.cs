using FluentAssertions;
using NUnit.Framework;

namespace TagCloud.Tests
{
    public class LiteratureTextParser
    {
        [Test]
        public void LiteratureTextParser_CorrectWorkOnSimpleFile()
        {
            var parser = new TagCloud.LiteratureTextParser(new PathCreater());
            var array = parser.GetWords("input.txt");
            array.Should().HaveCount(24);
            array.Should().Contain("кошка")
                .And.NotContain("Кошка")
                .And.Contain("собака")
                .And.Contain("заяц")
                .And.Contain("лев")
                .And.Contain("тигр")
                .And.Contain("жираф")
                .And.Contain("мышь")
                .And.Contain("волк");
        }
    }
}