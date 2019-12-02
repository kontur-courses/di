using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudForm.Tests
{
    [TestFixture]
    public class RectangleForWordsCreatorTests
    {
        [Test]
        public void FirstTest()
        {
            var creator = new RectangleForWordsCreator();
            var words = new Dictionary<string, int>
            {
                {"hello", 3 },
                {"first", 2 },
                {"hell", 1 }
            };
            var rects = creator.CreateRectanglesForWords(words);
            rects.Should().BeEmpty();
        }
    }
}
