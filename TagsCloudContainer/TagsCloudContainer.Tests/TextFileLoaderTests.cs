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

        [TestCase("123", TestName = "File not exist")]
        [TestCase(null, TestName = "null")]
        public void LoadText_ThrowsException_WhenFilename(string filename) =>
            Assert.Throws<ApplicationException>(() => loader.LoadText(filename));
    }
}