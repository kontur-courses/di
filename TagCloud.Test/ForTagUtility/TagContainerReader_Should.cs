using System;
using System.IO;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Utility.Models.Tag.Container;

namespace TagCloud.Tests.ForTagUtility
{
    [TestFixture]
    class TagContainerReader_Should : TestBase
    {
        private TagContainerReader sut;
        private const string TestFileName = "tmp.txt";

        [SetUp]
        public void SetUp()
        {
            sut = container.Resolve<TagContainerReader>();
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(TestFileName);
        }

        private void CreateTestText(string text)
        {
            if (File.Exists(TestFileName))
                File.Delete(TestFileName);
            using (var file = File.CreateText(TestFileName))
                file.WriteLine(text);
        }

        [Test]
        public void ReadTagContainer()
        {
            CreateTestText("TestGroup 0.0-1.0 1");

            var result = sut.ReadTagsContainer(TestFileName);

            result.Should().Contain(group => group.Item1 == "TestGroup"
                                             && group.Item2.FontSize == 1
                                             && group.Item2.FrequencyGroup.MinFrequencyCoef == 0
                                             && group.Item2.FrequencyGroup.MaxFrequencyCoef == 1);
        }

        [Test]
        public void ThrowArgumentException_WhenInputIncorrect()
        {
            CreateTestText("1 wrongFormat-1 1");

            Action read = () => sut.ReadTagsContainer(TestFileName);

            read.Should().Throw<ArgumentException>();
        }
    }
}