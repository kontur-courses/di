using System;
using System.IO;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Utility.Models.TextReader;

namespace TagCloud.Tests.ForTagUtility
{
    [TestFixture]
    public class TxtReader_Should : TestBase
    {
        private const string TestFileName = "tmp.txt";
        private TxtReader sut;

        [SetUp]
        public void SetUp()
        {
            if (File.Exists(TestFileName))
                File.Delete(TestFileName);
            using (var file = File.CreateText(TestFileName))
                file.WriteLine("text 123 words");
            sut = container.Resolve<TxtReader>();
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(TestFileName);
        }

        [TestCase("wrongFormat.bmp", TestName = "When wrong format")]
        [TestCase("NotExist.txt", TestName = "When file doesn't exists")]
        public void ThrowArgumentException(string path)
        {
            Action reading = () => sut.ReadToEnd(path);

            reading.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ReadWordsFromFile()
        {
            var result = sut.ReadToEnd(TestFileName);

            result.Should().BeEquivalentTo("text", "words", "123");
        }
    }
}