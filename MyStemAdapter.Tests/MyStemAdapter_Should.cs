using FluentAssertions;
using NUnit.Framework;
// ReSharper disable StringLiteralTypo

namespace MyStemAdapter.Tests
{
    public class MyStemAdapter_Should
    {
        [Test]
        public void DeterminePartOfSpeech()
        {
            var myStemAdapter = new MyStemAdapter();
            
            var info = myStemAdapter.GetWordInfo("магазин");

            info.PartOfSpeech.Should().Be(PartOfSpeech.Noun);
        }
        
        [Test]
        public void DetermineStem()
        {
            var myStemAdapter = new MyStemAdapter();
            
            var info = myStemAdapter.GetWordInfo("Вешалки");

            info.Stem.Should().Be("вешалка");
        }
    }
}
