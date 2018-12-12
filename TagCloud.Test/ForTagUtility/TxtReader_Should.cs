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

        [TestCase("wrongFormat.bmp",TestName = "When wrong format")]
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
            using (var file = File.CreateText("tmp.txt"))
                file.WriteLine("text 123 words");
            var reader = container.Resolve<TxtReader>();

            var result = reader.ReadToEnd("tmp.txt");

            result.Should().BeEquivalentTo("text", "words","123");
            File.Delete("tmp.txt");
        }
    }
}