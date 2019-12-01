using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using Autofac;

namespace TagsCloudContainer
{
    [TestFixture]
    class TagCloudGeneratorTests
    {
        [Test]
        public void CountWords()
        {
            var counter = new SimpleWordCounter();
            var words = new List<string> { "a", "d", "d", "j", "a", "h", "a" };
            var expect = new List<Tuple<string, int>>
            {
                new Tuple<string, int>("a", 3),
                new Tuple<string, int>("d", 2),
                new Tuple<string, int>("j", 1),
                new Tuple<string, int>("h", 1),
            };

            var result = counter.CountWords(words);

            result.Should().Equal(expect);
        }
    }
}
