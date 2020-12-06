using Autofac;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer;

namespace TagsCloudContainer_Tests
{
    [TestFixture]
    class TagsCloudContainer_Tests
    {
        private TagsCloudCreator creator;
        [SetUp]
        public void SetUp()
        {
            var scope = Configurator.GetContainer().BeginLifetimeScope();
            creator = scope.Resolve<TagsCloudCreator>();
        }

        [TestCase("")]
        [TestCase("asasas")]
        [TestCase("Tahomaaa")]
        public void TrySetFontFamily_ReturnsFalse_OnInvalidFontFamily(string fontFamily)
        {
            Assert.IsFalse(creator.TrySetFontFamily(fontFamily));
            Assert.AreEqual(creator.FontFamily, "Arial");
        }

        [TestCase("times new roman")]
        [TestCase("Tahoma")]
        public void TrySetFontFamily_ReturnsTrue_OnValidFontFamily(string fontFamily)
        {
            Assert.IsTrue(creator.TrySetFontFamily(fontFamily));
            Assert.AreEqual(creator.FontFamily.ToLower(), fontFamily.ToLower());
        }

        [TestCase("")]
        [TestCase("asasas")]
        [TestCase("синий")]
        public void TrySetFontColor_ReturnsFalse_OnInvalidColorName(string color)
        {
            Assert.IsFalse(creator.TrySetFontColor(color));
        }

        [TestCase("cyan")]
        [TestCase("Blue")]
        public void TrySetFontColor_ReturnsTrue_OnValidColorName(string color)
        {
            Assert.IsTrue(creator.TrySetFontColor(color));
        }

        [TestCase(0)]
        [TestCase(99)]
        [TestCase(2001)]
        public void TrySetImageSize_ReturnsFalse_OnInvalidSize(int size)
        {
            Assert.IsFalse(creator.TrySetImageSize(size));
        }

        [TestCase(1000)]
        [TestCase(2000)]
        [TestCase(100)]
        public void TrySetImageSize_ReturnsTrue_OnValidSize(int size)
        {
            Assert.IsTrue(creator.TrySetImageSize(size));
        }

        [TestCase("")]
        [TestCase("txt")]
        [TestCase("image")]
        public void TrySetImageFormat_ReturnsFalse_OnInvalidFormat(string imageFormat)
        {
            Assert.IsFalse(creator.TrySetImageFormat(imageFormat));
        }

        [TestCase("png")]
        [TestCase("jpeg")]
        public void TrySetImageFormat_ReturnsTrue_OnValidFormat(string imageFormat)
        {
            Assert.IsTrue(creator.TrySetImageFormat(imageFormat));
            Assert.AreEqual(creator.GetImageFormat(), imageFormat);
        }

        [Test]
        public void FixedColorProvider_ReturnsSameColor_OnManyCalls()
        {
            var color = Color.Red;
            var colorProvider = new FixedColorProvider(color);
            for (var i = 0; i < 5; i++)
                Assert.AreEqual(colorProvider.GetNextColor(), color);
        }

        [Test]
        public void StopWordsFilter_FilterAllStopWords_AndMakeLowerCase()
        {
            var filter = new StopWordsFilter();
            var input = new List<string> {"Abc", "of", "cba", "IN", "the", "car"};
            Assert.AreEqual(filter.Filter(input), new List<string> { "abc", "cba", "car" });
        }

        [Test]
        public void FontSizeByCount_CalculateFontSizeCorrectly()
        {
            var fontSizeByCount = new FontSizeByCount();
            var input = new List<string> { "abc", "abc", "abc", "ab", "ab", "a" };
            var wordsWithFont = fontSizeByCount.CalculateFontSize(input, "Arial").ToList();
            Assert.AreEqual(wordsWithFont[0].Font.Size, 35);
            Assert.AreEqual(wordsWithFont[1].Font.Size, 22.5);
            Assert.AreEqual(wordsWithFont[2].Font.Size, 10);
        }
    }
}
