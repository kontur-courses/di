using System;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.TextReaders;

namespace TagsCloudVisualization_Tests
{
    public class TextReader_Tests
    {
        [Test]
        public void TxtReaderGetText_FileNotFound_ShouldThrowArgumentException()
        {
            Action act = () => new TxtReader().ReadText("nonexistentName", Encoding.Default);
            act.Should().Throw<ArgumentException>();
        }
    }
}