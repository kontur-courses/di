using System;
using System.Collections.Generic;
using NUnit.Framework;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer.Tests
{
    public class SpeechPartWordsFilterTest
    {
        [Test]
        public void Constructor_ThrowsException_SpeechPartsToRemoveIsNull() =>
            Assert.Throws<ArgumentNullException>(() => new SpeechPartWordsFilter(new WordSpeechPartParser(), null));

        [Test]
        public void Constructor_ThrowsException_WordSpeechPartParserIsNull() =>
            Assert.Throws<ArgumentNullException>(() => new SpeechPartWordsFilter(null, new HashSet<SpeechPart>()));

        [Test]
        public void Filter_ThrowsException_WithWordsIsNull()
        {
            var filter = new SpeechPartWordsFilter(new WordSpeechPartParser(), new HashSet<SpeechPart>());

            Assert.Throws<ArgumentNullException>(() => filter.Filter(null));
        }
    }
}