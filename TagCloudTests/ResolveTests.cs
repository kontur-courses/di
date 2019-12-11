using System;
using Castle.Windsor;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.IServices;

namespace TagCloudTests
{
    [TestFixture]
    public class ResolveTests
    {
        [SetUp]
        public void SetUp()
        {
            container = TagCloud.Program.GetContainer();
        }

        private WindsorContainer container;

        [Test]
        public void Container_Should_ResolveIClientCorrectly()
        {
            Action resolve = () => container.Resolve<IClient>();
            resolve.Should().NotThrow();
        }
    }
}