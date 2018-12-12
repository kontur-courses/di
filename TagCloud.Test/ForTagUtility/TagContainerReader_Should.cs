using System;
using System.IO;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Utility.Container;
using TagCloud.Utility.Models.Tag.Container;

namespace TagCloud.Tests.ForTagUtility
{
    [TestFixture]
    class TagContainerReader_Should
    {
        private readonly IContainer container = ContainerConfig.StandartContainer;

        [Test]
        public void ReadTagContainer()
        {
            var tagContainerReader = container.Resolve<TagContainerReader>();
            using (var file = File.CreateText("tmp.txt"))
            {
                file.WriteLine("TestGroup 0.0-1.0 1;");
            }

            var result = tagContainerReader.ReadTagsContainer("tmp.txt");

            result.Should().Contain(group => group.Item1 == "TestGroup"
                                             && group.Item2.FontSize == 1
                                             && group.Item2.FrequencyGroup.MinFrequencyCoef == 0
                                             && group.Item2.FrequencyGroup.MaxFrequencyCoef == 1);
        }

        [Test]
        public void ThrowArgumentException_WhenInputIncorrect()
        {
            var tagContainerReader = container.Resolve<TagContainerReader>();
            using (var file = File.CreateText("tmp.txt"))
            {
                file.WriteLine("1 wrongFormat-1 1;");
            }

            Action read = () => tagContainerReader.ReadTagsContainer("tmp.txt");

            read.Should().Throw<ArgumentException>();
            File.Delete("tmp.txt");
        }
    }
}
