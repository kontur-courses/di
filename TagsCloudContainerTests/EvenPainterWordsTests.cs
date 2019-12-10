using NUnit.Framework;
using System.Drawing;
using TagsCloudContainer.PaintersWords;
using TagsCloudContainer.TokensAndSettings;

namespace TagsCloudContainerTests
{
    [TestFixture]
    class EvenPainterWordsTests
    {
        [Test]
        public void GetNextBrush_OddNumber_Aqua()
        {
            var evenPainterWords = new EvenPainterWords();

            var result = evenPainterWords.GetNextBrush(new WordToken("qqq", 1));

            Assert.AreEqual(Brushes.Aqua, result);
        }

        [Test]
        public void GetNextBrush_EvenNumber_Red()
        {
            var evenPainterWords = new EvenPainterWords();

            var result = evenPainterWords.GetNextBrush(new WordToken("qqq", 6));

            Assert.AreEqual(Brushes.Red, result);
        }
    }
}
