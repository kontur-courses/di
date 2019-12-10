using NUnit.Framework;
using System.Drawing;
using TagsCloudContainer.PaintersWords;
using TagsCloudContainer.TokensAndSettings;

namespace TagsCloudContainerTests
{
    [TestFixture]
    class SimplePainterWordsTests
    {
        [Test]
        public void GetNextBrush_ConstantBrush()
        {
            var simplePainterWords = new SimplePainterWords(Brushes.Red);

            var result = simplePainterWords.GetNextBrush(new WordToken("qqq", 1));

            Assert.AreEqual(Brushes.Red, result);
        }
    }
}
