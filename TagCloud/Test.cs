using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloud
{
    public class Test
    {
        [Test]
        public void OneWordInLineParser_CorrectWorkOnSimpleFile()
        {
            var parser = new LiteratureTextParser(new PathCreater());
            var array = parser.GetWords("input.txt");
            array.Should().HaveCount(26);
            array.Should().Contain("кошка")
                .And.Contain("собака")
                .And.Contain("заяц")
                .And.Contain("лев")
                .And.Contain("тигр")
                .And.Contain("жираф")
                .And.Contain("мышь")
                .And.Contain("волк");
        }

        [Test]
        public void Canvas_HasCorrectCenter()
        {
            var canvas = new Canvas(400, 600);
            canvas.Center.Should().BeEquivalentTo(new Point(200, 300));
        }
        
    }
}