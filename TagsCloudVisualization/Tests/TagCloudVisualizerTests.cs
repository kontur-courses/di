using System;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class TagCloudVisualizerTests
    {
        [Test]
        public void TagCloudVisualizer_NotFailsOnLoad() //If preprocessor module file exists
        {
            Action action = () => TagCloudVisualizerEntryPoint.Main(new string[0]);
            action.Should().NotThrow();
        }
    }
}