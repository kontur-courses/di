using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagCloudTests
{
    [TestFixture]
    public class TagCloudFactoryTests
    {
        private TagCloudFactory factory = new TagCloudFactory();

        [TestCase("sorted")]
        [TestCase("mixed")]
        public void TagCloudFactory_NotThrows(string order)
        {
            Action create = () => factory.CreateInstance(false, order);
            create.Should().NotThrow();
        }
        
        [TestCase("qwery")]
        [TestCase("Mixed")]
        [TestCase("Shuffled")]
        public void TagCloudFactory_UnknownOrder_Throws(string order)
        {
            Action create = () => factory.CreateInstance(false, order);
            create.Should().Throw<ArgumentException>();
        }
    }
}