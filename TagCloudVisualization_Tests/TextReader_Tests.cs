using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagCloudVisualization_Tests
{
    public class TextReader_Tests
    {
        [Test]
        public void TextReaderCtor_FileNotFound_ShouldThrowArgumentException()
        {
            Action act = () => new TxtReader("nonexistentName");
            act.Should().Throw<ArgumentException>();
        }
    }
}