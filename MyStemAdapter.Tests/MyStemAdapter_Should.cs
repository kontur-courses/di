using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace MyStemAdapter.Tests
{
    [TestFixture]
    public class MyStemAdapter_Should
    {
        [Test]
        public async Task DeterminePartOfSpeech()
        {
            var myStemAdapter = new MyStemAdapter();
            
            var info = await myStemAdapter.GetWordInfo("магазин");

            info.PartOfSpeech.Should().Be(PartOfSpeech.Noun);
        }
        
        [Test]
        public async Task DetermineStem()
        {
            var myStemAdapter = new MyStemAdapter();
            
            var info = await myStemAdapter.GetWordInfo("Вешалки");

            info.Stem.Should().Be("вешалка");
        }
    }
}
