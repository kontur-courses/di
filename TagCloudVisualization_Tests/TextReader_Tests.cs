using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.PathFinders;
using TagsCloudVisualization.TextReaders;

namespace TagsCloudVisualization.Tests
{
    public class TextReader_Tests
    {
        [Test]
        public void TxtReaderGetText_FileNotFound_ShouldThrowArgumentException()
        {
            Action act = () => new TxtReader().ReadText("nonexistentName");
            act.Should().Throw<ArgumentException>();
        }
    }
}