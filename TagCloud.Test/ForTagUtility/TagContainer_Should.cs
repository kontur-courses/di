using System;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Utility.Container;
using TagCloud.Utility.Models.Tag;
using TagCloud.Utility.Models.Tag.Container;

namespace TagCloud.Tests.ForTagUtility
{
    [TestFixture]
    class TagContainer_Should
    {
        private readonly IContainer container = ContainerConfig.StandartContainer;

        [TestCase("Big", 1, 1, 1, "exist", TestName = "When group with same name already exists")]
        [TestCase("group", 1, 1, 0, "Font", TestName = "When font size is zero")]
        [TestCase("group", 1, 1, -3, "Font", TestName = "When font size is negative")]
        [TestCase("group", 0, 1, 1, "intersect", TestName = "When group intersects with other")]
        public void MethodAddThrowsArgumentException(string name, double minVal, double maxVal, int fontSize, string exceptionKeyWord)
        {
            var tagContainer = container.Resolve<TagContainer>();

            Action add = () => tagContainer.Add(name, new FrequencyGroup(minVal, maxVal), fontSize);

            add.Should().Throw<ArgumentException>()
                .And.Message.Should().Contain(exceptionKeyWord);
        }

        [Test]
        public void RemoveGroupByName()
        {
            var tagContainer = container.Resolve<TagContainer>();
            tagContainer.Add("group", new FrequencyGroup(1, 1), 1);

            tagContainer.Remove("group");

            tagContainer.Should().NotContain(group => group.Item1 == "group");
        }
    }
}
