using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    public class TextFileReaderTests
    {
        [Test]
        public void ReadFile_ShouldThrowException_WhenInvalidPath()
        {
            var reader = new TextFileReader();
            var read = new Action(() => reader.ReadFile(string.Empty));
            read.Should().Throw<ArgumentException>();
        }
    }
}