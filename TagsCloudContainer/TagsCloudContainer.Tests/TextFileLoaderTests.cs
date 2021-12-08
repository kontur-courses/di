using System;
using Moq;
using NUnit.Framework;
using TagsCloudContainer.WordsLoading;

namespace TagsCloudContainer.Tests
{
    public class TextFileLoaderTests
    {
        private TextFileLoader loader;

        [SetUp]
        public void SetUp()
        {
            loader = new Mock<TextFileLoader>().Object;
        }

        [Test]
        public void LoadText_ThrowsException_WhenFilename() =>
            Assert.Throws<ApplicationException>(() => loader.LoadText("notExistingFile"));
    }
}