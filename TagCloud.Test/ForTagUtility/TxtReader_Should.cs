using System;
using System.IO;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Utility.Container;
using TagCloud.Utility.Models.TextReader;

namespace TagCloud.Tests.ForTagUtility
{
    [TestFixture]
    public class TxtReader_Should 
    {
        private readonly IContainer container = ContainerConfig.StandartContainer;
        private const string TestFileName = "tmp.txt";

        [SetUp]
        public void SetUp()
        {
            if (File.Exists(TestFileName))
                File.Delete(TestFileName);
            using (var file = File.CreateText(TestFileName))
                file.WriteLine("text 123 words");
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
            var reader = container.Resolve<TxtReader>();

            Action reading = () => reader.ReadToEnd(path);

            reading.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ReadWordsFromFile()
        {
            var reader = container.Resolve<TxtReader>();

            var result = reader.ReadToEnd(TestFileName);

            result.Should().BeEquivalentTo("text", "words", "123");
        }
    }
}