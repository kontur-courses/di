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
    public class SpellCheckerTests
    {
        [Test]
        public void FilterTest()
        {
            var checker = new SpellCheckerFilter();
            var words = new string[] {"www", "hello", "asd"};
            checker.Filter(words, LanguageEnum.English).Should().BeEquivalentTo(new string[] { "hello" });
        }
    }
}
