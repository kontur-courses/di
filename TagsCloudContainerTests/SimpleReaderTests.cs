using NUnit.Framework;
using TagsCloudContainer.Readers;

namespace TagsCloudContainerTests
{
    [TestFixture]
    class SimpleReaderTests
    {
        [Test]
        public void ReadAllLines_Docx()
        {
            var simpleReader = new SimpleReader(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"TagsCloudContainerTests\Words.docx"));

            var result = simpleReader.ReadAllLines();

            var expect = new[] { "A", "A", "A", "A", "D", "D", "B", "B", "D", "A" };

            Assert.AreEqual(expect.Length, result.Length);
            Assert.AreEqual(expect, result);
        }

        [Test]
        public void ReadAllLines_Doc()
        {
            var simpleReader = new SimpleReader(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"TagsCloudContainerTests\Words.doc"));

            var result = simpleReader.ReadAllLines();

            var expect = new[] { "A", "A", "A", "A", "D", "D", "B", "B", "D", "A", "D" };

            Assert.AreEqual(expect.Length, result.Length);
            Assert.AreEqual(expect, result);
        }

        [Test]
        public void ReadAllLines_txt()
        {
            var simpleReader = new SimpleReader(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"TagsCloudContainerTests\Words.txt"));

            var result = simpleReader.ReadAllLines();

            var expect = new[] { "A", "A", "A", "A", "D", "D", "B", "B", "D", "A"};

            Assert.AreEqual(expect.Length, result.Length);
            Assert.AreEqual(expect, result);
        }
    }
}
