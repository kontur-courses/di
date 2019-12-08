using NUnit.Framework;

namespace TagsCloudContainer.Readers
{
    [TestFixture]
    class SimpleReaderTests
    {
        [Test]
        public void ReadAllLines_Docx()
        {
            var simpleReader = new SimpleReader(@"E:\Projects\Shpora1\di\TagsCloudContainer\Words.docx");

            var result = simpleReader.ReadAllLines();

            var expect = new[] { "A", "A", "A", "A", "D", "D", "B", "B", "D", "A" };

            Assert.AreEqual(expect.Length, result.Length);
            Assert.AreEqual(expect, result);
        }

        [Test]
        public void ReadAllLines_Doc()
        {
            var simpleReader = new SimpleReader(@"E:\Projects\Shpora1\di\TagsCloudContainer\Words.doc");

            var result = simpleReader.ReadAllLines();

            var expect = new[] { "A", "A", "A", "A", "D", "D", "B", "B", "D", "A", "D" };

            Assert.AreEqual(expect.Length, result.Length);
            Assert.AreEqual(expect, result);
        }
    }
}
