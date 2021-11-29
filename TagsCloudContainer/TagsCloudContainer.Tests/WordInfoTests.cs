using System;
using NUnit.Framework;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer.Tests
{
    public class WordInfoTests
    {
        [Test]
        public void Constructor_WithNull_ThrowsException() =>
            Assert.That(() => new WordInfo(null, SpeechPart.S), Throws.InstanceOf<ArgumentException>());
    }
}